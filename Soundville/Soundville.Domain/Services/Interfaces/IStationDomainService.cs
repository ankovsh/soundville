using System.Collections.Generic;
using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IStationDomainService : IDomainService<Station>
    {
        Station GetStationById(int id);
        IList<Station> GetAllStationsByUser(string userEmail);
        IList<Station> GetAllStations();
        IList<Station> GetStationsByName(string name);
        void Save(Station station);
    }
}
