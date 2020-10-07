using Microsoft.AspNetCore.Http;
using QReduction.Core.Domain.Acl;

namespace QReduction.Apis.Models
{
    public class RegistrationModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Email { get; set; }
        //public string UserGuid { get; set; }
        public UserTypes? UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
    }

    public class ExternalRegistrationModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public ProviderTypes ProviderType { get; set; }

        public string ProviderToken { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile UserImage { get; set; }

        public string Email { get; set; }
     
        public UserTypes? UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
    }

    public enum ProviderTypes
    {
        Google = 1,
        Facebook = 2,
        Twitter = 3
    }
}
