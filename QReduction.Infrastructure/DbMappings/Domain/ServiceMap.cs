using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class ServiceMap : CustomEntityTypeConfiguration<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameAr).HasMaxLength(50).IsRequired();
            builder.Property(c => c.NameEn).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(505);

            builder.Property(c => c.CreateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.UpdateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.DeletedAt).HasColumnType("smalldatetime");
            builder.HasOne(k => k.Organization).WithMany().HasForeignKey(fk => fk.OrganizationId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}