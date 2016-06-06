using LunarSFX.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
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
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {

    }
}
