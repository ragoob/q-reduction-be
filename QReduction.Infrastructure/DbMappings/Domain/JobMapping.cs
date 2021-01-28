using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace QReduction.Infrastructure.DbMappings.Domain
{
    public class JobMapping :  CustomEntityTypeConfiguration<Job>
    {
        public override void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.JobRequests).WithOne(c => c.Job).HasForeignKey(c=> c.JobId);
            
            base.Configure(builder);
        }
    }
}
