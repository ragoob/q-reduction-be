using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Acl;
using System.ComponentModel.DataAnnotations.Schema;

namespace QReduction.Infrastructure.DbMappings.Domain.BasicData
{
    public class PagePermissionMap : CustomEntityTypeConfiguration<PagePermission>
    {
        public override void Configure(EntityTypeBuilder<PagePermission> builder)
        {
            builder.ToTable("PagePermissions", "Acl");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

            builder.HasOne(r => r.PermissionsTerm).WithMany().HasForeignKey(f => f.PermissionTermId).IsRequired();
            builder.HasOne(r => r.Page).WithMany().HasForeignKey(f => f.PageId).IsRequired();
            builder.Property(r => r.ACLName).HasMaxLength(50).IsRequired();

            base.Configure(builder);
        }
    }
}
