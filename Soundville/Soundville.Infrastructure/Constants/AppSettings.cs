using System;
using System.Configuration;
using System.Globalization;

namespace Soundville.Infrastructure.Constants
{
    public static class AppSettings
    {
        public static string VkClientSecret
        {
            get
            {
                return Setting<string>("CLIENT_SECRET");
            }
        }

        public static string VkClientId
        {
            get
            {
                return Setting<string>("CLIENT_ID");
            }
        }

        public static string SiteUrl
        {
            get
            {
                return Setting<string>("SITE_URL");
            }
        }

        private static T Setting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}
