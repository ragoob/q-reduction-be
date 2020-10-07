using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Acl;
using System.ComponentModel.DataAnnotations.Schema;

namespace QReduction.Infrastructure.DbMappings.Domain.BasicData
{
    public class PermissionsTermMap : CustomEntityTypeConfiguration<PermissionsTerm>
    {
        public override void Configure(EntityTypeBuilder<PermissionsTerm> builder)
        {
            builder.ToTable("PermissionsTerms", "Acl");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

            builder.Property(p => p.NameAr).HasMaxLength(50).IsRequired();
            builder.Property(p => p.NameEn).HasMaxLength(50).IsRequired();

            base.Configure(builder);
        }
    }
}
