using System.Collections.Generic;
using System.Linq;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class StationSongDomainService : DomainService<StationSong>, IStationSongDomainService
    {
        public StationSongDomainService(ISoundvilleContext soundvilleContext) 
            : base(soundvilleContext)
        {
        }

        public IList<StationSong> GetAllStationSongByStation(int stationId)
        {
            return Context.StationSongs.Where(x => x.StationId == stationId).ToList();
        }
    }
}
