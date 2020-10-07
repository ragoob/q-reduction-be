using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Acl;

namespace QReduction.Infrastructure.DbMappings.Domain.BasicData
{
    public class UserRoleMap : CustomEntityTypeConfiguration<UserRole>
    {

        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles", "Acl");
            builder.HasKey(k => k.Id);
            builder.HasOne(r => r.Role).WithMany().HasForeignKey(r => r.RoleId).IsRequired();
            builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId).IsRequired();

            base.Configure(builder);
        }
    }
}
