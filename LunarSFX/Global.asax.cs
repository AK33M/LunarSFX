using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using System.Web;

namespace LunarSFX
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            // default MVC stuff
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);            
        }

    }
    //protected override IKernel CreateKernel()
    //{
    //    var kernel = new StandardKernel();

    //    kernel.Load(new RepositoryModule());
    //    kernel.Bind<IBlogRepository>().To<EFBlogRepository>();

    //    return kernel;
    //}

    //protected override void OnApplicationStarted()
    //{
    //    // Code that runs on application startup
    //    AreaRegistration.RegisterAllAreas();
    //    GlobalConfiguration.Configure(WebApiConfig.Register);
    //    RouteConfig.RegisterRoutes(RouteTable.Routes);
    //    BundleConfig.RegisterBundles(BundleTable.Bundles);
    //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    //    base.OnApplicationStarted();
    //}
}
