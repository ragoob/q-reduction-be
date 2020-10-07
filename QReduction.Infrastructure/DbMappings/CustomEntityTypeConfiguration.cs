using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QReduction.Infrastructure.DbMappings
{
    public class CustomEntityTypeConfiguration<TEntity> : IModelConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

        }
    }
}
