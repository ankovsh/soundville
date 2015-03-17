using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class StationSongDomainService : DomainService<StationSong>, IStationSongDomainService
    {
        protected StationSongDomainService(ISoundvilleContext soundvilleContext) 
            : base(soundvilleContext)
        {
        }
    }
}
