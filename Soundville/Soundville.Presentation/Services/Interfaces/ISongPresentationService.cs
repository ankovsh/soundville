using Soundville.Presentation.Models.Songs;

namespace Soundville.Presentation.Services.Interfaces
{
    public interface ISongPresentationService : IPresentationService
    {
        void Save(SongSaveModel model);
        string Vote(int stationSongId, int value, string email);
    }
}
