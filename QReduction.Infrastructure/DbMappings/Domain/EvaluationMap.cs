using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class EvaluationMap : CustomEntityTypeConfiguration<Evaluation>
    {
        public override void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            builder.ToTable("Evaluations");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Comment).HasMaxLength(500);

            builder.Property(c => c.CreateAt).HasColumnType("smalldatetime");
         
            builder.Property(c => c.DeletedAt).HasColumnType("smalldatetime");
            builder.HasOne(k => k.ShiftQueue).WithMany().HasForeignKey(fk => fk.ShiftQueueId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}