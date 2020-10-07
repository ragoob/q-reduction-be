using QReduction.Core.UnitOfWork;
using QReduction.Infrastructure.DbContexts;

namespace QReduction.Infrastructure.UnitOfWorks
{
    public interface IQReductionUnitOfWork : IUnitOfWork
    {
        IDatabaseContext DatabaseContext { get; }
    }
}
