using System.Threading.Tasks;

namespace QReduction.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
