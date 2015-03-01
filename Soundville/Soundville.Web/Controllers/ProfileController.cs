using System.Web.Mvc;
using Soundville.Presentation.Models.Profile;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfilePresentationService _profilePresentationService;

        public ProfileController(IProfilePresentationService profilePresentationService)
        {
            _profilePresentationService = profilePresentationService;
        }

        [HttpGet]
        public ActionResult Edit()
        {
            ProfileEditModel model = _profilePresentationService.GetProfileEditModel(User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProfileEditModel model)
        {
            _profilePresentationService.Save(model, User.Identity.Name);
            return null;
        }
    }
}