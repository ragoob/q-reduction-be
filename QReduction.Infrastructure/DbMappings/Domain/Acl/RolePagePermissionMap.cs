using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Acl;

namespace QReduction.Infrastructure.DbMappings.Domain.BasicData
{
    public class RolePagePermissionMap : CustomEntityTypeConfiguration<RolePagePermission>
    {
        public override void Configure(EntityTypeBuilder<RolePagePermission> builder)
        {
            builder.ToTable("RolePagePermissions", "Acl");
            builder.HasKey(k => k.Id);
            builder.HasOne(r => r.Role).WithMany().HasForeignKey(f => f.RoleId).IsRequired();
            builder.HasOne(r => r.PagePermission).WithMany().HasForeignKey(f => f.PagePermissionId).IsRequired();
            base.Configure(builder);
        }
    }
}
