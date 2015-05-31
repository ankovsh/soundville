using System.Collections.Generic;
using System.Linq;

namespace Soundville.Presentation.Models.Stations
{
    public class ViewStationModel
    {
        public StationItem Station { get; set; }
        public IList<StationSongItem> Songs { get; set; }

        public ViewStationModel(StationItem station, IList<StationSongItem> songs)
        {
            Station = station;
            Songs = songs.OrderBy(x => x.Position).ToList();
        }
    }
}
