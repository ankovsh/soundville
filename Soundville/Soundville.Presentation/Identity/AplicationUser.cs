using Microsoft.AspNet.Identity;

namespace Soundville.Presentation.Identity
{
    public class ApplicationUser : IUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
