using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Infrastructure.DbMappings.Domain
{
    public class ShiftUserViewMap : CustomEntityTypeConfiguration<ShiftUserView>
    {
        public override void Configure(EntityTypeBuilder<ShiftUserView> builder)
        {
            builder.ToTable("ShiftUserView");
            builder.HasKey(c => c.Id);
        }
    }
}
