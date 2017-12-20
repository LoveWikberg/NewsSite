using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace NewsSite.Data
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public int? Age { get; set; }

        [NotMapped]
        public string Role { get; set; }

        [NotMapped]
        public IList<Claim> Claims { get; set; }

        [NotMapped]
        public string[] PublishRights { get; set; }
    }

}