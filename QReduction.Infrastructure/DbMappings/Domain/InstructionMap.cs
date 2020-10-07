using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class InstructionMap : CustomEntityTypeConfiguration<Instruction>
    {
        public override void Configure(EntityTypeBuilder<Instruction> builder)
        {
            builder.ToTable("Instructions");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameAr).HasMaxLength(50).IsRequired();
            builder.Property(c => c.NameEn).HasMaxLength(50).IsRequired();
            builder.HasOne(k => k.Organization).WithMany().HasForeignKey(fk => fk.OrganizationId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.CreateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.UpdateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.DeletedAt).HasColumnType("smalldatetime");
        }
    }
}