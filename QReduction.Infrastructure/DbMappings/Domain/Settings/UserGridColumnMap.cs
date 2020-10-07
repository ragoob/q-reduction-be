using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using QReduction.Infrastructure.DbMappings;
using QReduction.Core.Domain.Settings;

namespace QReduction.Infrastructure.DbMappings.Domain.Settings
{
    public class UserGridColumnMap : CustomEntityTypeConfiguration<UserGridColumn>
    {
        public override void Configure(EntityTypeBuilder<UserGridColumn> builder)
        {
            builder.ToTable("UserGridColumns", "Settings");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.IsVisible).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.SystemGridColumnId).IsRequired();
            builder.HasOne(r => r.SystemGridColumn).WithMany().HasForeignKey(f => f.SystemGridColumnId).IsRequired();
            base.Configure(builder);
        }
    }
}


