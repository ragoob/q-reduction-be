using Microsoft.EntityFrameworkCore;

namespace QReduction.Infrastructure.DbMappings
{
    public interface IModelConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
