using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using QReduction.Core.Domain.Acl;

namespace QReduction.Infrastructure.DbMappings.Domain.BasicData
{
    public class PageMap : CustomEntityTypeConfiguration<Page>
    {
        public override void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Pages", "Acl");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

            builder.Property(p => p.NameAr).HasMaxLength(50).IsRequired();
            builder.Property(p => p.NameEn).HasMaxLength(50).IsRequired();

            base.Configure(builder);
        }
    }
}
