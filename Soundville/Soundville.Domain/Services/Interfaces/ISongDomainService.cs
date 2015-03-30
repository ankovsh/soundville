using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface ISongDomainService : IDomainService<Song>
    {
        Song GetSongById(int id);
        Song GetByArtistAndTitle(string artist, string title);
        void Save(Song song);
    }
}
