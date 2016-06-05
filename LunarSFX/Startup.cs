using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace LunarSFX
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           //app.CreatePerOwinContext(ApplicationDbContext.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });
        }
    }
}
