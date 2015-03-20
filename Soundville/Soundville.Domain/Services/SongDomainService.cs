using System.Linq;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class SongDomainService : DomainService<Song>, ISongDomainService
    {
        public SongDomainService(ISoundvilleContext soundvilleContext) 
            : base(soundvilleContext)
        {
        }

        public Song GetSongById(int id)
        {
            return Context.Songs.SingleOrDefault(x => x.Id == id);
        }
    }
}
