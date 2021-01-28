using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Infrastructure.DbMappings.Domain
{
    public class JobRequestMap : CustomEntityTypeConfiguration<JobRequest>
    {
        public override void Configure(EntityTypeBuilder<JobRequest> builder)
        {
            builder.ToTable("JobRequests");

            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.JobRequestParameters).WithOne(c => c.JobRequest).HasForeignKey(c => c.JobRequestId);

            base.Configure(builder);
        }
    }
}
