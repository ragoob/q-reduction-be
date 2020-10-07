using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Settings;
using System.ComponentModel.DataAnnotations.Schema;

namespace QReduction.Infrastructure.DbMappings.Domain.Setting
{
    public class SystemGridMap : CustomEntityTypeConfiguration<SystemGrid>
    {
        public override void Configure(EntityTypeBuilder<SystemGrid> builder)
        {
            builder.ToTable("SystemGrids", "Settings");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);


            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            base.Configure(builder);
        }
    }
}


