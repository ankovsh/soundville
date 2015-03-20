using System.Collections.Generic;
using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IStationSongDomainService : IDomainService<StationSong>
    {
        IList<StationSong> GetAllStationSongByStation(int stationId);
    }
}
