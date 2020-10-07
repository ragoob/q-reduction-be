using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class BranchMap : CustomEntityTypeConfiguration<Branch>
    {
        public override void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branchs");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameAr).HasMaxLength(50).IsRequired();
            builder.Property(c => c.NameEn).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Phone).HasMaxLength(50);
            builder.Property(c => c.BranchAddress).HasMaxLength(500);
            builder.Property(c => c.Note).HasMaxLength(500);

            builder.Property(c => c.CreateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.UpdateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.DeletedAt).HasColumnType("smalldatetime");
        }
    }
}