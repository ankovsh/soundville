using System.Web.Mvc;
using Soundville.Presentation.Services;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserPresentationService _userPresentationService;

        public HomeController(/*IUserPresentationService userPresentationService*/)
        {
            //_userPresentationService = userPresentationService;
            _userPresentationService = new UserPresentationService();
        }

        public ViewResult Index()
        {
            ViewBag.UserName = _userPresentationService.GetFirstUserName();
            return View();
        }
    }
}