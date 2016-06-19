using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using System.Web;
using System;
using LunarSFX.Controllers;

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

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((HttpApplication)sender).Context;
            var ex = Server.GetLastError();
            var status = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;

            // Is Ajax request? return json
            if (httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                httpContext.ClearError();
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = status;
                httpContext.Response.TrySkipIisCustomErrors = true;
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Write("{ success: false, message: \"Error occured in server.\" }");
                httpContext.Response.End();
            }
            else
            {
                var currentController = " ";
                var currentAction = " ";
                var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

                if (currentRouteData != null)
                {
                    if (currentRouteData.Values["controller"] != null &&
                        !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                    {
                        currentController = currentRouteData.Values["controller"].ToString();
                    }

                    if (currentRouteData.Values["action"] != null &&
                        !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                    {
                        currentAction = currentRouteData.Values["action"].ToString();
                    }
                }

                var controller = new ErrorController();
                var routeData = new RouteData();

                httpContext.ClearError();
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = status;
                httpContext.Response.TrySkipIisCustomErrors = true;

                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = status == 404 ? "NotFound" : "Index";

                controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
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
