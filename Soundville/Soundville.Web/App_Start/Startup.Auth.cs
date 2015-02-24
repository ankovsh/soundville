using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Soundville.Presentation.Identity;

namespace Soundville.Web
{
    public partial class Startup
    {
        public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; set; }

        public Startup()
        {
            UserManagerFactory = () =>
            {
                var userManager = new UserManager<ApplicationUser>(new CustomUserStore());
                return userManager;
            };
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/LogOff"),
                ExpireTimeSpan = TimeSpan.FromDays(7)
            });

            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}