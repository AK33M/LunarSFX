using LunarSFX.Providers;
using System.Security.Claims;
using System.Web.Mvc;

namespace LunarSFX.Controllers
{
    public abstract class AppController : Controller
    {
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }
    }
}