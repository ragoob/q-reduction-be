using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QReduction.Core.Domain.Settings;
using QReduction.Core.Extensions;
using QReduction.Core.Repository.Custom;
using QReduction.Core.Repository.Generic;
using QReduction.Core.Service.Custom;
using QReduction.Infrastructure.UnitOfWorks;
using QReduction.Services.Generic;

namespace QReduction.Services.Custom
{
    public class UserGridColumnService : Service<UserGridColumn>, IUserGridColumnService
    {

        #region Fields 
        private readonly IRepository<SystemGridColumn> _gridColumnRepository;
        private IUserGridColumnRepository UserGridColumnRepository
        {
            get
            {
                return (IUserGridColumnRepository)this._repository;
            }
        }
        #endregion

        #region ctor
        public UserGridColumnService(
            IQReductionUnitOfWork unitOfWork,
            IUserGridColumnRepository userGridColumnRepository,
            IRepository<SystemGridColumn> gridColumnRepository
            )
            : base(unitOfWork, userGridColumnRepository)
        {
            _gridColumnRepository = gridColumnRepository;
        }

        #endregion

        #region Methods

        public List<UserGridColumn> GetUserGrid(string gridName, int userId)
        {
            List<UserGridColumn> userColumns = new List<UserGridColumn>();

            IEnumerable<SystemGridColumn> gridColumns =
                _gridColumnRepository
                .Find(g => g.SystemGrid.Name == gridName,
                "SystemGrid");

            IEnumerable<UserGridColumn> userGridColumns =
                UserGridColumnRepository
                .Find(ug => ug.UserId == userId &&
                      ug.SystemGridColumn.SystemGrid.Name == gridName,
                 "SystemGridColumn", "SystemGrid");

            gridColumns.ForEach(c =>
            {
                UserGridColumn userCol = userGridColumns.Where(ug => ug.SystemGridColumnId == c.Id).FirstOrDefault();

                if (userCol == null)
                    userColumns.Add(new UserGridColumn
                    {
                        IsVisible = c.VisibilityDefault,
                        SystemGridColumnId = c.Id,
                        UserId = userId,
                        SystemGridColumn = c
                    });
                else
                    userColumns.Add(userCol);

            });

            return userColumns;
        }
        public async Task<List<UserGridColumn>> GetUserGridAsync(string gridName, int userId)
        {
            List<UserGridColumn> userColumns = new List<UserGridColumn>();

            IEnumerable<SystemGridColumn> gridColumns =
               await _gridColumnRepository
               .FindAsync(g => g.SystemGrid.Name == gridName,
               "SystemGrid");

            IEnumerable<UserGridColumn> userGridColumns =
               await UserGridColumnRepository
               .FindAsync(ug => ug.UserId == userId &&
                      ug.SystemGridColumn.SystemGrid.Name == gridName,
                "SystemGridColumn", "SystemGridColumn.SystemGrid");

            gridColumns.ForEach(c =>
            {
                UserGridColumn userCol = userGridColumns.Where(ug => ug.SystemGridColumnId == c.Id).FirstOrDefault();

                if (userCol == null)
                    userColumns.Add(new UserGridColumn
                    {
                        IsVisible = c.VisibilityDefault,
                        SystemGridColumnId = c.Id,
                        UserId = userId,
                        SystemGridColumn = c
                    });
                else
                    userColumns.Add(userCol);

            });

            return userColumns;
        }

        public void AddUserGridColumns(List<UserGridColumn> userGridColumns, string gridName)
        {
            int userId = userGridColumns[0].UserId;

            IEnumerable<UserGridColumn> savedUserGridColumns = UserGridColumnRepository
               .Find(ug => ug.UserId == userId &&
                      ug.SystemGridColumn.SystemGrid.Name == gridName);

            UserGridColumnRepository.RemoveRange(savedUserGridColumns);
            UserGridColumnRepository.AddRange(userGridColumns);
            _unitOfWork.SaveChanges();
        }

        public async Task AddUserGridColumnsAsync(List<UserGridColumn> userGridColumns, string gridName)
        {
            int userId = userGridColumns[0].UserId;

            IEnumerable<UserGridColumn> savedUserGridColumns = await UserGridColumnRepository
               .FindAsync(ug => ug.UserId == userId &&
                      ug.SystemGridColumn.SystemGrid.Name == gridName);

            UserGridColumnRepository.RemoveRange(savedUserGridColumns);
            UserGridColumnRepository.AddRange(userGridColumns);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion


    }
}

