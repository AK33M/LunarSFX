using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace LunarSFX.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("LunarSFXDbConnString", throwIfV1Schema: false)
        {

        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }

    public class AppUser : IdentityUser
    {
        //Add custom user claims here
        //public string Country { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, UserName));

            return userIdentity;
        }
    }
}