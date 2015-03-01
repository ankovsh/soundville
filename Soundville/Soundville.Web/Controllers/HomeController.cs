using System.Web.Mvc;

namespace Soundville.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            return View();
        }

        public ViewResult GetInTouch()
        {
            return View();
        }
    }
}