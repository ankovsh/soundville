using System.Web;
using Soundville.Domain.Models;

namespace Soundville.Presentation.Models.Profile
{
    public class ProfileEditModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public ProfileEditModel()
        {
        }

        public ProfileEditModel(User user)
        {
            Name = user.Name;
            Email = user.Email;
        }
    }
}
