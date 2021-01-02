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
    public class ShiftRepository : Repository<Shift>, IShiftRepository ,IDisposable
    {
        private readonly DbSet<Shift> _shiftSet;


        public ShiftRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
           
            _shiftSet = databaseContext.Set<Shift>();
        }

        public void Dispose()
        {
            //databaseContext.
        }

        public IQueryable<Shift> GetBranchOpenShiftIds(int BranchId, string CurrentTime)
        {
            var _BranchId = new SqlParameter("@BranchId", BranchId);
            var _Time = new SqlParameter("@CurrentTime", dbType: System.Data.SqlDbType.Time) { SqlValue = CurrentTime };
            return _shiftSet.FromSql($"GetBranchOpenShiftIds @BranchId,@CurrentTime ", _BranchId, _Time);
            // throw new NotImplementedException();
        }
    }
}
