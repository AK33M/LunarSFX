using LunarSFX.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LunarSFX
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
        }

        //public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        //{
        //    var manager = new AppUserManager(new UserStore<AppUser>(context.Get<AppDbContext>()));

        //    //Configure validation logic for usernames
        //    manager.UserValidator = new UserValidator<AppUser>(manager)
        //    {
        //        AllowOnlyAlphanumericUserNames = false,
        //        RequireUniqueEmail = true
        //    };

        //    //Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = false,
        //        RequireDigit = false,
        //        RequireLowercase = false,
        //        RequireUppercase = false
        //    };

        //    // Configure user lockout defaults
        //    manager.UserLockoutEnabledByDefault = true;
        //    manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //    manager.MaxFailedAccessAttemptsBeforeLockout = 5;

        //    //// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        //    //// You can write your own provider and plug it in here.
        //    //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<AppUser>
        //    //{
        //    //    MessageFormat = "Your security code is {0}"
        //    //});
        //    //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AppUser>
        //    //{
        //    //    Subject = "Security Code",
        //    //    BodyFormat = "Your security code is {0}"
        //    //});
        //    //manager.EmailService = new EmailService();
        //    //manager.SmsService = new SmsService();

        //    var dataProtectionProvider = options.DataProtectionProvider;
        //    if (dataProtectionProvider != null)
        //    {
        //        manager.UserTokenProvider =
        //            new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }
        //    return manager;
        //}
    }

    public class AppSignInManager : SignInManager<AppUser, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }

        //public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        //{
        //    return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        //}
    }

    public class AppRoleManager : RoleManager<IdentityRole>
    {
        public AppRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}