using System.Web.Mvc;
using Soundville.Infrastructure.Constants;

namespace Soundville.Web.Controllers
{
    public class StationSongController : Controller
    {
        public ActionResult SearchPage(bool? isAuthenticated)
        {
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
                return View();
            }

            return RedirectToAction("VkAuth", "Account", new
            {
                redirectUrl = AppSettings.SiteUrl + "StationSong/SearchPage?isAuthenticated=true"
            });
        }

        public ActionResult Search(string searchString)
        {
            return Json(new[] { new { Id = 1, Value = "Splin" }, new { Id = 2, Value = "Agata" } }, JsonRequestBehavior.AllowGet);
        }
    }
}