using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace QReduction.Core.Domain.Acl
{
    public class User
    {
        public int Id { get; set; }
        public Guid UserGuid { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime? LastLoginUtcDate { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhotoPath { get; set; }
        public string ForgotPasswordCode { get; set; }
        public DateTime? ForgotPasswordExpiration { get; set; }

        public DateTime? LastUpdateDate { get; set; }
        public Guid RowGuid { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserTypes? UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VerificationCode { get; set; }
        public bool IsVerified { get; set; }

        public bool IsFirstLogin { get; set; }

        public string UserDeviceId { get; set; }
        public DateTime? VerificationCodeExpiration { get; set; }
        public int ? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual ICollection<LoginProviders> LoginProviders { get; set; }
        
       


        
    }

    public class LoginProviders
    {
        public long Id { get; set; }


        public int ProviderType { get; set; }

        public string Providertoken { get; set; }


    }

    public class About
    {
        public int Id { get; set; }

        public string LabelTextAr { get; set; }

        public string LabelValueAr { get; set; }

        public string LabelValueEn { get; set; }

        public string LabelTextEn { get; set; }

        public int? CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class HelpAndSupport
    {
        public int Id { get; set; }

        public string MessageTitle { get; set; }
        public string Message { get; set; }

        public int? UserId { get; set; }

        public virtual User User { get; set; }


        public int? CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

    }


}
