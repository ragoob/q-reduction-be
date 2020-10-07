using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;

namespace QReduction.Infrastructure.DbMappings.Domain
{

    public class BranchServiceMap : CustomEntityTypeConfiguration<BranchService>
    {
        public override void Configure(EntityTypeBuilder<BranchService> builder)
        {
            builder.ToTable("BranchServices");
            builder.HasKey(c => c.Id);

            builder.HasOne(k => k.Branch).WithMany(k=> k.BranchServices).HasForeignKey(fk => fk.BranchId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(k => k.Service).WithMany().HasForeignKey(fk => fk.ServiceId).OnDelete(DeleteBehavior.Restrict);
        }
	}

    public class AboutMap : CustomEntityTypeConfiguration<About>
    {
        public override void Configure(EntityTypeBuilder<About> builder)
        {
            builder.ToTable("About");
            builder.HasKey(c => c.Id);

           
        }
    }

    public class HelpAndSupportMap : CustomEntityTypeConfiguration<HelpAndSupport>
    {
        public override void Configure(EntityTypeBuilder<HelpAndSupport> builder)
        {
            builder.ToTable("HelpAndSupport");
            builder.HasKey(c => c.Id);


        }
    }
}