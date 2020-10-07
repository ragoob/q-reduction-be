using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QReduction.Infrastructure.DbContexts;

namespace QReduction.Infrastructure.UnitOfWorks
{
    public class QReductionUnitOfWork : IQReductionUnitOfWork
    {
        #region Properties
        public IDatabaseContext DatabaseContext { get; private set; }
        #endregion

        #region ctor
        public QReductionUnitOfWork(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }
        #endregion

        #region Execute
        public int SaveChanges()
        {
            return DatabaseContext.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return DatabaseContext.SaveChangesAsync();
        }
        #endregion
    }
}
