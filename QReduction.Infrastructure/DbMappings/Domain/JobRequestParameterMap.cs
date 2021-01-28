using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Infrastructure.DbMappings.Domain
{
    public class JobRequestParameterMap : CustomEntityTypeConfiguration<JobRequestParameter>
    {
        public override void Configure(EntityTypeBuilder<JobRequestParameter> builder)
        {
            builder.ToTable("JobRequestParameters");
            builder.HasKey(c => c.Id);
            base.Configure(builder);
        }
    }
}
