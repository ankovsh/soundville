using System.Collections.Generic;
using Soundville.Domain.Models;

namespace Soundville.Presentation.Models.Stations
{
    public class StationListModel
    {
        public IList<StationItem> Stations { get; set; }

        public StationListModel(IList<Station> stations)
        {
            Stations = new List<StationItem>();
            foreach (var station in stations)
            {
                Stations.Add(new StationItem(station));
            }
        }
    }
}
