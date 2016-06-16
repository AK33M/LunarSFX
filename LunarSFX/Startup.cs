using LunarSFX.App_Start;
using Microsoft.Owin;
using Owin;
using SimpleInjector;
using System.Web.Mvc;
using LunarSFX.Core.Objects;

[assembly: OwinStartup(typeof(LunarSFX.Startup))]
namespace LunarSFX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = SimpleInjectorInitializer.Initialize(app);
            ConfigureAuth(app, container);
            ConfigureCustomModelBinder(container);
        }

        private void ConfigureCustomModelBinder(Container container)
        {
            ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(container));
        }
    }
}
