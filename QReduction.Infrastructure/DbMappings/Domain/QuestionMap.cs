using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class QuestionMap : CustomEntityTypeConfiguration<Question>
    {
        public override void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.QuestionTextAr).HasMaxLength(250).IsRequired();
            builder.Property(c => c.QuestionTextEn).HasMaxLength(250).IsRequired();

            builder.HasOne(k => k.Organization).WithMany().HasForeignKey(fk => fk.OrganizationId).OnDelete(DeleteBehavior.Restrict);


            builder.Property(c => c.CreateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.UpdateAt).HasColumnType("smalldatetime");
            builder.Property(c => c.DeletedAt).HasColumnType("smalldatetime");
        }


    }

    public class EvaluationQuestionAnswerMap : CustomEntityTypeConfiguration<EvaluationQuestionAnswer>
    {
        public override void Configure(EntityTypeBuilder<EvaluationQuestionAnswer> builder)
        {
            builder.ToTable("EvaluationQuestionAnswer");
            builder.HasKey(c => c.Id);

            
        }


    }
}