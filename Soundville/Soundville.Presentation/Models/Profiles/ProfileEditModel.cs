using System.ComponentModel.DataAnnotations;
using System.Web;
using Soundville.Domain.Models;

namespace Soundville.Presentation.Models.Profiles
{
    public class ProfileEditModel
    {
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImageFileName { get; set; }

        public ProfileEditModel()
        {
        }

        public ProfileEditModel(User user)
        {
            Name = user.Name;
            Email = user.Email;
            ImageFileName = user.ImageFileName;
        }
    }
}
