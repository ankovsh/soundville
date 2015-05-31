using System.Collections.Generic;
using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IStationSongDomainService : IDomainService<StationSong>
    {
        IList<StationSong> GetAllStationSongByStation(int stationId);
        int GetLastSongPosition(int stationId);
        StationSong GetByPosition(int stationId, int position);
        void Save(StationSong stationSong);
        bool IsExist(int stationSongId);
    }
}
