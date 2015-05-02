using System;
using System.IO;
using System.Web.Mvc;
using Castle.Core.Internal;
using Soundville.Infrastructure.Constants;
using Soundville.Infrastructure.Utils;
using Soundville.Presentation.Models.Profiles;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Web.Controllers
{
    [Authorize]
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
            ViewBag.ImageSrc = model.ImageFileName.IsNullOrEmpty()
                ? "/Content/Images/male-default-avatar.png"
                : Path.Combine(ImageConstants.AvatarUrl, model.ImageFileName);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProfileEditModel model)
        {
            // TODO: проверка существования папки UserContent/avatars
            // TODO: удаление старой аватарки из папки

            if (model.Image != null && !ImageConstants.ValidImageTypes.Contains(model.Image.ContentType))
            {
                ModelState.AddModelError(
                    NameOf<ProfileEditModel>.Property(x => x.Image),
                    "Please choose either a GIF, JPG or PNG image.");
                ViewBag.ImageSrc = "/Content/Images/male-default-avatar.png";
            }

            if (ModelState.IsValid)
            {
                string newImageName;
                if (model.Image != null)
                {
                    string imageExtension = Path.GetExtension(model.Image.FileName);
                    newImageName = Guid.NewGuid() + imageExtension;
                    var imagePath = Path.Combine(Server.MapPath(ImageConstants.AvatarDir), newImageName);
                    model.Image.SaveAs(imagePath);
                }
                else newImageName = null;
                
                _profilePresentationService.Save(model, User.Identity.Name, newImageName);
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }
    }
}