using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Soundville.Presentation.Models.Stations;

namespace Soundville.Presentation.Models.Profiles
{
    public class ProfileModel
    {
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImageFileName { get; set; }
        public IList<StationItem> StationModels { get; set; }  
    }
}
