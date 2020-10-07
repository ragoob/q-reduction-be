using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Settings;

namespace QReduction.Infrastructure.DbMappings.Domain.Setting
{
    public class SystemGridColumnMap : CustomEntityTypeConfiguration<SystemGridColumn>
    {
        public override void Configure(EntityTypeBuilder<SystemGridColumn> builder)
        {
            builder.ToTable("SystemGridColumns", "Settings");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();

            builder.Property(p => p.VisibilityDefault).IsRequired();
            builder.Property(p => p.SystemGridId).IsRequired();
            builder.HasOne(r => r.SystemGrid).WithMany().HasForeignKey(f => f.SystemGridId).IsRequired();
            base.Configure(builder);
        }
    }
}


