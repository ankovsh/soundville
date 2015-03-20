using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface ISongDomainService : IDomainService<Song>
    {
        Song GetSongById(int id);
    }
}
