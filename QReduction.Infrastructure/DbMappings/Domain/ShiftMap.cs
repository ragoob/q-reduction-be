using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class ShiftMap : CustomEntityTypeConfiguration<Shift>
    {
        public override void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("Shifts");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.BranchId).HasMaxLength(50).IsRequired();
            builder.Property(c => c.StartAt).HasColumnType("smalldatetime");
            builder.Property(c => c.EndAt).HasColumnType("smalldatetime");

            builder.Property(c => c.CreateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.UpdateAt).HasColumnType("smalldatetime");

            builder.HasOne(k => k.Branch).WithMany().HasForeignKey(fk => fk.BranchId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(k => k.UserSupport).WithMany().HasForeignKey(fk => fk.UserIdSupport).OnDelete(DeleteBehavior.Restrict);
        }
    }
}