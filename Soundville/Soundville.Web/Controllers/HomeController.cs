using System.Web.Mvc;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserPresentationService _userPresentationService;

        public HomeController(IUserPresentationService userPresentationService)
        {
            _userPresentationService = userPresentationService;
        }

        public ViewResult Index()
        {
            ViewBag.UserName = _userPresentationService.GetFirstUserName();
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