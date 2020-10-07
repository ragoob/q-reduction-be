using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class ShiftUserMap : CustomEntityTypeConfiguration<ShiftUser>
    {
        public override void Configure(EntityTypeBuilder<ShiftUser> builder)
        {
            builder.ToTable("ShiftUsers");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.WindowNumber).HasMaxLength(50);

            builder.HasOne(k => k.Shift).WithMany().HasForeignKey(fk => fk.ShiftId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(k => k.User).WithMany().HasForeignKey(fk => fk.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}