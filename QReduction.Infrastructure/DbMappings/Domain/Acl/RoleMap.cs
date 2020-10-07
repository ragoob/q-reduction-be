using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Acl;

namespace QReduction.Infrastructure.DbMappings.Domain.BasicData
{
    public class RoleMap : CustomEntityTypeConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "Acl");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.NameAr).HasMaxLength(50).IsRequired();
            builder.Property(p => p.NameEn).HasMaxLength(50).IsRequired();
            base.Configure(builder);
        }
    }
}
