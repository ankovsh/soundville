using System.Collections.Generic;
using System.Linq;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class StationDomainService : DomainService<Station>, IStationDomainService
    {
        public StationDomainService(ISoundvilleContext soundvilleContext)
            : base(soundvilleContext)
        {
        }

        public Station GetStationById(int id)
        {
            return Context.Stations.SingleOrDefault(x => x.Id == id);
        }

        public IList<Station> GetAllStationsByUser(int userId)
        {
            return Context.Stations.Where(x => x.UserId == userId).ToList();
        }

        public void Save(Station station)
        {
            if (station.Id == 0)
            {
                Context.Set<Station>().Add(station);
            }

            Context.SaveChanges();
        }
    }
}
