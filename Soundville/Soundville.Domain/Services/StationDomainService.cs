using System.Collections.Generic;
using System.Linq;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class StationDomainService : DomainService<Station>, IStationDomainService
    {
        public StationDomainService()
            : base(new SoundvilleContext())
        {
        }

        public StationDomainService(ISoundvilleContext soundvilleContext)
            : base(soundvilleContext)
        {
        }

        public Station GetStationById(int id)
        {
            return Context.Stations.SingleOrDefault(x => x.Id == id);
        }

        public IList<Station> GetAllStationsByUser(string userEmail)
        {
            return Context.Stations.Where(x => x.User.Email == userEmail).ToList();
        }

        public IList<Station> GetAllStations()
        {
            return Context.Stations.Select(x => x).ToList();
        }

        public IList<Station> GetStationsByName(string name)
        {
            return Context.Stations.Where(x => x.Name == name).ToList();
        }

        public bool IsOwner(int id, string userEmail)
        {
            return Context.Stations.Any(x => x.User.Email == userEmail && x.Id == id);
        }

        public IList<Station> GetSignedStationsBySubscriber(string subscriberEmail)
        {
            return Context.Stations.Where(s => s.Subscribers.Any(u => u.Email == subscriberEmail)).ToList();
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
