using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Acl;

namespace QReduction.Infrastructure.DbMappings.Domain.Acl
{
    public class UserTypeMap : CustomEntityTypeConfiguration<UserType>
    {
        public override void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserTypes", "Acl");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.NameAr).HasMaxLength(225);
            builder.Property(p => p.NameEn).HasMaxLength(255);
            base.Configure(builder);
        }
    }
}
