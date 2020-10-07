using QReduction.Core.Domain.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;
using QReduction.Core.Service.Generic;

namespace QReduction.Core.Service.Custom
{
    public interface IUserGridColumnService : IService<UserGridColumn>
    {
        List<UserGridColumn> GetUserGrid(string gridName, int userId);
        Task<List<UserGridColumn>> GetUserGridAsync(string gridName, int userId);


        void AddUserGridColumns(List<UserGridColumn> userGridColumns, string gridName);
        Task AddUserGridColumnsAsync(List<UserGridColumn> userGridColumns, string gridName);
    }
}
