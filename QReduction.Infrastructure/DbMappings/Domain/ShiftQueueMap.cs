using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class ShiftQueueMap : CustomEntityTypeConfiguration<ShiftQueue>
    {
        public override void Configure(EntityTypeBuilder<ShiftQueue> builder)
        {
            builder.ToTable("ShiftQueues");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.WindowNumber).HasMaxLength(50);

            builder.HasOne(k => k.Shift).WithMany().HasForeignKey(fk => fk.ShiftId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(k => k.UserBy).WithMany().HasForeignKey(fk => fk.UserIdBy).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(k => k.UserMobile).WithMany().HasForeignKey(fk => fk.UserIdMobile).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(k => k.Service).WithMany().HasForeignKey(fk => fk.ServiceId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}