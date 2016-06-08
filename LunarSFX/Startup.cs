using LunarSFX.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LunarSFX.Startup))]
namespace LunarSFX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = SimpleInjectorInitializer.Initialize(app);
            ConfigureAuth(app, container);
        }
    }
}
