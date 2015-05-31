using System.Collections.Generic;
using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IStationDomainService : IDomainService<Station>
    {
        IList<Station> GetAllStationsByUser(string userEmail);
        IList<Station> GetAllStations();
        IList<Station> GetStationsByName(string name);
        bool IsOwner(int id, string userEmail);
        IList<Station> GetSignedStationsBySubscriber(string subscriberEmail);
        void Save(Station station);
    }
}
