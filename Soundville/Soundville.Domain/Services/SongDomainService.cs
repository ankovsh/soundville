using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class SongDomainService : DomainService<Song>, ISongDomainService
    {
        protected SongDomainService(ISoundvilleContext soundvilleContext) 
            : base(soundvilleContext)
        {
        }
    }
}
