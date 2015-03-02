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

        public const string AvatarDir = "/UserContent/avatars";
        public const string StationAvatarDir = "/UserContent/stationAvatars";
    }
}
