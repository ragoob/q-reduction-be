using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QReduction.Core.Domain.Acl;

namespace QReduction.Infrastructure.DbMappings.Domain.BasicData
{
    public class UserMap : CustomEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "Acl");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.UserGuid).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(225);
            builder.Property(p => p.UserName).HasMaxLength(255);
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.PhoneNumber).HasMaxLength(50);
            builder.Property(c => c.ForgotPasswordCode).HasMaxLength(6).IsRequired(false);
            builder.Property(c => c.ForgotPasswordExpiration).IsRequired(false);
            builder.Property(c => c.VerificationCode).HasMaxLength(4);
            builder.Property(c => c.IsVerified).HasDefaultValue(false);
            builder.Property(c => c.VerificationCodeExpiration).IsRequired(false);



            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.UserName).IsUnique(false);

            builder.HasOne(k => k.UserType).WithMany().HasForeignKey(fk => fk.UserTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(k => k.Organization).WithMany().HasForeignKey(fk => fk.OrganizationId).OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
