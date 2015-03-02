using System.Collections.Generic;
using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IStationDomainService : IDomainService<Station>
    {
        Station GetStationById(int id);
        IList<Station> GetAllStationsByUser(int userId);
        void Save(Station station);
    }
}
