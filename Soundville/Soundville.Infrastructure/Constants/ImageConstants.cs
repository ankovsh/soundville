using System.Collections.Generic;

namespace Soundville.Infrastructure.Constants
{
    public class ImageConstants
    {
        public static IList<string> ValidImageTypes = new List<string> {
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png"
        };

        public static string AvatarDir = "/UserContent/avatars";
        public static string AvatarUrl = AppSettings.SiteUrl + AvatarDir;
        public static string StationAvatarDir = "/UserContent/stationAvatars";
        public static string StationAvatarUrl = AppSettings.SiteUrl + StationAvatarDir;
    }
}
