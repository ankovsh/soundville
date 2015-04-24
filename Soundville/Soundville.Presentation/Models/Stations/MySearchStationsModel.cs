using System.Collections.Generic;
using Soundville.Domain.Models;

namespace Soundville.Presentation.Models.Stations
{
    public class MySearchStationsModel
    {
        public IList<StationItem> Stations { get; set; }

        public MySearchStationsModel(IList<Station> stations)
        {
            Stations = new List<StationItem>();
            foreach (var station in stations)
            {
                Stations.Add(new StationItem(station));
            }
        }
    }
}
