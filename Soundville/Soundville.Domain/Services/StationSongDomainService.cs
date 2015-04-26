using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class StationSongDomainService : DomainService<StationSong>, IStationSongDomainService
    {
        public StationSongDomainService()
            : base(new SoundvilleContext())
        {
        }

        public StationSongDomainService(ISoundvilleContext soundvilleContext) 
            : base(soundvilleContext)
        {
        }

        public IList<StationSong> GetAllStationSongByStation(int stationId)
        {
            return Context.StationSongs.Where(x => x.StationId == stationId).OrderBy(y => y.Position).ToList();
        }

        public int GetLastSongPosition(int stationId)
        {
            var stationSongs = GetAllStationSongByStation(stationId);
            return stationSongs.IsNullOrEmpty() ? 0 : stationSongs.Max(y => y.Position);
        }

        public StationSong GetByPosition(int stationId, int position)
        {
            return Context.StationSongs.SingleOrDefault(x => x.Station.Id == stationId && x.Position == position);
        }

        public void Save(StationSong stationSong)
        {
            if (stationSong.Id == 0)
            {
                Context.Set<StationSong>().Add(stationSong);
            }

            Context.SaveChanges();
        }
    }
}
