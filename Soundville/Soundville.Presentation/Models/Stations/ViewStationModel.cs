using System.Collections.Generic;

namespace Soundville.Presentation.Models.Stations
{
    public class ViewStationModel
    {
        public StationItem Station { get; set; }
        public IList<StationSongItem> Songs { get; set; }

        public ViewStationModel(StationItem station, IList<StationSongItem> songs)
        {
            Station = station;
            Songs = songs;
        }
    }
}
