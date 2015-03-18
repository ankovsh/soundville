using System.ComponentModel.DataAnnotations;
using Soundville.Domain.Models;

namespace Soundville.Presentation.Models.Stations
{
    public class StationItem
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string ImageFileName { get; set; } 

        public StationItem()
        {
        }

        public StationItem(Station station)
        {
            Name = station.Name;
            ImageFileName = station.ImageFileName;
        }
    }
}
