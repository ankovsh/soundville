using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Soundville.Infrastructure.Constants;
using Soundville.Infrastructure.SocialNetwork;
using Soundville.Presentation.Models.Profiles;
using Soundville.Presentation.Models.Songs;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Web.Controllers
{
    public class StationSongController : Controller
    {
        private readonly IProfilePresentationService _profilePresentationService;
        private readonly ISongPresentationService _songPresentationService;

        public StationSongController(IProfilePresentationService profilePresentationService, ISongPresentationService songPresentationService)
        {
            _profilePresentationService = profilePresentationService;
            _songPresentationService = songPresentationService;
        }

        public async Task<ActionResult> SearchPage(int id, bool? isAuthenticated, string code)
        {
            var redirectUrl = AppSettings.SiteUrl + "StationSong/SearchPage?id=" + id + "&isAuthenticated=true";
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
                var token = await VkHelper.GetTokenAsync(code, redirectUrl);
                
                _profilePresentationService.SaveVkModel(new ProfileVkModel(token), User.Identity.Name);
                ViewBag.StationId = id;
                return View();
            }

            return RedirectToAction("VkAuth", "Account", new
            {
                redirectUrl = redirectUrl
            });
        }

        public async Task<ActionResult> SearchAsync(string searchString)
        {
            var token = _profilePresentationService.GetTokenByEmail(User.Identity.Name);
            var api = VkHelper.GetApi(token);

            var songs = await api.Audio.Search(searchString);
            
            return Json(songs, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddSongAsync(int stationId, int songId, int ownerVkId)
        {
            var token = _profilePresentationService.GetTokenByEmail(User.Identity.Name);
            var api = VkHelper.GetApi(token);

            var song = (await api.Audio.GetById(false, ownerVkId + "_" + songId)).FirstOrDefault();

            const string songExtension = ".mp3";
            string newSongName = Guid.NewGuid() + songExtension;
            var songPath = Path.Combine(Server.MapPath(SongConstants.SongDir), newSongName);

            WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(new Uri(song.Url), songPath);

            var model = new SongSaveModel();
            model.FileName = newSongName;
            model.SongUrl = songPath;
            model.Artist = song.Artist;
            model.Duration = song.Duration;
            model.Title = song.Title;
            model.StationId = stationId;

            _songPresentationService.Save(model);

            return null;
        }
    }
}