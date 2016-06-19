using System.Web.Mvc;

namespace LunarSFX.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult NotFound()
        {
            return View();
        }
    }
}