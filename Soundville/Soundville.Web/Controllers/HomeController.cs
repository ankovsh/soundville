using System.Web.Mvc;

namespace Soundville.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("MyStations", "Station");
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