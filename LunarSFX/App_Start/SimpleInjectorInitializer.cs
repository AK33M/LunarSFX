using LunarSFX.Core.Repositories;
using LunarSFX.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LunarSFX.App_Start
{
    public static class SimpleInjectorInitializer
    {
        public static Container Initialize(IAppBuilder app)
        {
            var container = GetInitializeContainer(app);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }

        private static Container GetInitializeContainer(IAppBuilder app)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.RegisterPerWebRequest<AppUserManager>();
            container.RegisterPerWebRequest<AppSignInManager>();

            //TODO: Role Manager
            container.RegisterPerWebRequest<AppRoleManager>();
            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>>(
                    () => new RoleStore<IdentityRole>(
                  container.GetInstance<AppDbContext>()));

            container.RegisterPerWebRequest<LunarSFXDbContext>();
            container.RegisterPerWebRequest<IAuthenticationManager>(() => 
                AdvancedExtensions.IsVerifying(container)
                    ? new OwinContext(new Dictionary<string, object>()).Authentication
                    : HttpContext.Current.GetOwinContext().Authentication);
            container.RegisterPerWebRequest<AppDbContext>(() => 
                new AppDbContext("LunarSFXDbConnString"));
            container.RegisterPerWebRequest<IUserStore<AppUser>>(() => 
                new UserStore<AppUser>(container.GetInstance<AppDbContext>()));

            // Register your stuff here  
            container.Register<IBlogRepository, EFBlogRepository>(Lifestyle.Scoped);

            container.RegisterInitializer<AppUserManager>(manager => InitializeUserManager(manager, app));

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            return container;
        }

        private static void InitializeUserManager(AppUserManager manager, IAppBuilder app)
        {
            //Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            //Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = app.GetDataProtectionProvider();

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }
}
