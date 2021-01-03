using Microsoft.EntityFrameworkCore;
using QReduction.Core.Domain;
using QReduction.Core.Repository.Custom;
using QReduction.Infrastructure.DbContexts;
using QReduction.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace QReduction.Infrastructure.Repositories.Custom
{
    public class ShiftUserViewRepository : Repository<ShiftUserView>, IShiftUserViewRepository
    {
        private readonly DbSet<ShiftUserView> _shiftUserSet;

        public ShiftUserViewRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
            _shiftUserSet = databaseContext.Set<ShiftUserView>();
        }

        public IQueryable<ShiftUserView> ShiftUserPerDay(int UserId, int BranchId, string CurrentTime)
        {
            var _UserId = new SqlParameter("@UserId",UserId);
            var _BranchId = new SqlParameter("@BranchId", BranchId);
            var _Time = new SqlParameter("@CurrentTime", dbType: System.Data.SqlDbType.Time) { SqlValue = CurrentTime };
            return _shiftUserSet.FromSql($"ShiftUserPerDay @UserId, @BranchId,@CurrentTime ", _UserId, _BranchId, _Time).AsNoTracking() ;
            
        }
    }
}
