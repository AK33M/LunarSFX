using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Ninject.Web.Common;
using Ninject;
using LunarSFX.Core.Repositories;
using System.Web.Optimization;

namespace LunarSFX
{
    public class Global : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new RepositoryModule());
            kernel.Bind<IBlogRepository>().To<EFBlogRepository>();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            base.OnApplicationStarted();
        }
    }
}