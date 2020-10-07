using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Core.Domain.Acl.FacebookModels
{
    public class FacebookLoginResource
    {
        [Required]
        [StringLength(255)]
        public string facebookToken { get; set; }
    }

    public class FacebookUserResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
    }

    public class TokenResource
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
    }
    public class AuthorizationTokensResource
    {
        public TokenResource AccessToken { get; set; }
        public TokenResource RefreshToken { get; set; }
    }

}
