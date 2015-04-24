using System;
using System.IO;
using System.Web.Mvc;
using Castle.Core.Internal;
using Soundville.Infrastructure.Constants;
using Soundville.Infrastructure.Utils;
using Soundville.Presentation.Models.Stations;
using Soundville.Presentation.Services.Interfaces;
using Soundville.Presentation.Streaming;

namespace Soundville.Web.Controllers
{
    [Authorize]
    public class StationController : Controller
    {
        private readonly IStationPresentationService _stationPresentationService;

        public StationController(IStationPresentationService stationPresentationService)
        {
            _stationPresentationService = stationPresentationService;
        }

        [HttpGet]   
        public ActionResult Edit(int? id)
        {
            StationEditModel model = _stationPresentationService.GetStationEditModel(id);
            ViewBag.ImageSrc = model.ImageFileName.IsNullOrEmpty()
                ? "/Content/Images/male-default-avatar.png"
                : Path.Combine(ImageConstants.StationAvatarUrl, model.ImageFileName);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StationEditModel model)
        {
            if (model.Image == null || model.Image.ContentLength == 0)
            {
                ModelState.AddModelError(
                    NameOf<StationEditModel>.Property(x => x.Image),
                    "This field is required");
            }
            else if (!ImageConstants.ValidImageTypes.Contains(model.Image.ContentType))
            {
                ModelState.AddModelError(
                    NameOf<StationEditModel>.Property(x => x.Image),
                    "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                string imageExtension = Path.GetExtension(model.Image.FileName);
                string newImageName = Guid.NewGuid() + imageExtension;
                var imagePath = Path.Combine(Server.MapPath(ImageConstants.StationAvatarDir), newImageName);
                model.Image.SaveAs(imagePath);
                _stationPresentationService.Save(model, newImageName, User.Identity.Name);
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }

        [HttpGet]
        public ActionResult MyStations()
        {
            MyStationsModel model = _stationPresentationService.GetMyStationsModel(User.Identity.Name);
            ViewBag.DefaultImageSrc = "/Content/Images/male-default-avatar.png";
            ViewBag.PartialImageUrl = ImageConstants.StationAvatarUrl + "/";
            return View(model);
        }

        [HttpGet]
        public ActionResult ViewStation(int id)
        {   
            var model = _stationPresentationService.GetViewStationModel(id);
            ViewBag.Title = model.Station.Name;
            ViewBag.DefaultImageSrc = "/Content/Images/male-default-avatar.png";
            ViewBag.PartialImageUrl = ImageConstants.StationAvatarUrl + "/";

            return View(model);
        }

        [HttpGet]
        public void StartStream(int id)
        {
            var mp3StreamingPool = Mp3StreamingPool.Instance;
            if (!mp3StreamingPool.IsStreamExist(id))
            {
                mp3StreamingPool.StartMp3Streaming(id, Server.MapPath(SongConstants.SongDir), 128);
            }

            var mp3Stream = mp3StreamingPool.GetStream(id);
            mp3Stream.PlaySong();
        }

        [HttpGet]
        public ActionResult SearchStations()
        {
            MySearchStationsModel model = _stationPresentationService.GetSearchStationsModel();
            ViewBag.DefaultImageSrc = "/Content/Images/male-default-avatar.png";
            ViewBag.PartialImageUrl = ImageConstants.StationAvatarUrl + "/";
            return View(model);
        }

        public ActionResult Search(string searchString)
        {
            var stations = _stationPresentationService.GetSearchStationsModelByName(searchString);
            ViewBag.DefaultImageSrc = "/Content/Images/male-default-avatar.png";
            ViewBag.PartialImageUrl = ImageConstants.StationAvatarUrl + "/";
            return Json(stations, JsonRequestBehavior.AllowGet);
        }
    }
}