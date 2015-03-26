using System.Threading.Tasks;
using System.Web.Mvc;
using Soundville.Infrastructure.Constants;
using Soundville.Infrastructure.SocialNetwork;
using Soundville.Presentation.Models.Profiles;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Web.Controllers
{
    public class StationSongController : Controller
    {
        private readonly IProfilePresentationService _profilePresentationService;

        public StationSongController(IProfilePresentationService profilePresentationService)
        {
            _profilePresentationService = profilePresentationService;
        }

        public async Task<ActionResult> SearchPage(bool? isAuthenticated, string code)
        {
            var redirectUrl = AppSettings.SiteUrl + "StationSong/SearchPage?isAuthenticated=true";
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
                var token = await VkHelper.GetTokenAsync(code, redirectUrl);
                
                _profilePresentationService.SaveVkModel(new ProfileVkModel(token), User.Identity.Name);

                return View();
            }

            return RedirectToAction("VkAuth", "Account", new
            {
                redirectUrl = redirectUrl
            });
        }

        public ActionResult Search(string searchString)
        {
            return Json(new[] { new { Id = 1, Value = "Splin" }, new { Id = 2, Value = "Agata" } }, JsonRequestBehavior.AllowGet);
        }
    }
}