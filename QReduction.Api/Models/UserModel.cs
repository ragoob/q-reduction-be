using Microsoft.AspNetCore.Http;
using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using System.Collections.Generic;

namespace QReduction.Apis.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Email { get; set; }
        public string UserGuid { get; set; }
        public bool IsActive { get; set; }

        public IFormFile UserImage { get; set; }
        public UserTypes? UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public int ? OrganizationId { get; set; }
        public string PhotoPath { get; set; }
        public int? branchId { get; set; }
    }

    #region Models


    public class ScanResponse
    {
        public string Message { get; set; }

        public ScanData data { get; set; }
    }

    public class ScanData
    {
        public Organization Organization { get; set; }

        public Branch Branch { get; set; }


        public List<BranchService> Serivces { get; set; }


        public List<Instruction> Instructions { get; set; }
    }


    public class UserDeviceRequest
    {
        public string DeviceId { get; set; }

        public string UserGuid { get; set; }
    }



    public class SelectServiceRequest
    {
        public string BranchId { get; set; }

        public string ServiceId { get; set; }
    }

    public class UserDeviceResponse
    {
        public string Message { get; set; }
        public User data { get; set; }
    }

    public class SendInstantMessageRequest
    {
        public string UserGuid { get; set; }


    }
    public class SendInstantMessageResponse
    {
        public string Message { get; set; }


    }


    #endregion #Models
}
