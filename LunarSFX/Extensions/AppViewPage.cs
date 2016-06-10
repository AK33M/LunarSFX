using LunarSFX.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LunarSFX.Extensions
{
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        protected AppUserPrincipal CurrentUser
        {
            get
            {
                return new AppUserPrincipal(User as ClaimsPrincipal);
            }
        }

        //protected string Greeting
        //{
        //    get
        //    {
        //        //return HttpContext.GetGlobalResourceObject("Labels", "Greeting", new System.Globalization.CultureInfo("en")).ToString();
        //        return Resources.Labels.Greeting;
        //    }
        //}
        //protected string Abuse
        //{
        //    get
        //    {
        //        //return HttpContext.GetGlobalResourceObject("Labels", "Abuse", new System.Globalization.CultureInfo("en")).ToString();
        //        return Resources.Labels.Abuse;
        //    }
        //}
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {

    }
}
