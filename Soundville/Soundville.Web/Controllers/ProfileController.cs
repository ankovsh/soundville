﻿using System;
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
            if (model.ImageFileName.IsNullOrEmpty())
            {
                ViewBag.ImageSrc = "/Content/Images/male-default-avatar.png";
            }
            ViewBag.ImageSrc = Path.Combine(ImageConstants.AvatarDir, model.ImageFileName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProfileEditModel model)
        {
            // TODO: проверка существования папки UserContent/avatars
            // TODO: удаление старой аватарки из папки

            if (model.Image == null || model.Image.ContentLength == 0)
            {
                ModelState.AddModelError(
                    NameOf<ProfileEditModel>.Property(x => x.Image),
                    "This field is required");
            }
            else if (!ImageConstants.ValidImageTypes.Contains(model.Image.ContentType))
            {
                ModelState.AddModelError(
                    NameOf<ProfileEditModel>.Property(x => x.Image),
                    "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                string imageExtension = Path.GetExtension(model.Image.FileName);
                string newImageName = Guid.NewGuid() + imageExtension;
                var imagePath = Path.Combine(Server.MapPath(ImageConstants.AvatarDir), newImageName);
                model.Image.SaveAs(imagePath);
                _profilePresentationService.Save(model, User.Identity.Name, newImageName);
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }
    }
}