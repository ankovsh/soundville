using System.ComponentModel.DataAnnotations;
using System.Web;
using Soundville.Domain.Models;

namespace Soundville.Presentation.Models.Stations
{
    public class StationEditModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string ImageFileName { get; set; }

        public StationEditModel()
        {
        }

        public StationEditModel(Station station)
        {
            Name = station.Name;
            ImageFileName = station.ImageFileName;
        }
    }
}
