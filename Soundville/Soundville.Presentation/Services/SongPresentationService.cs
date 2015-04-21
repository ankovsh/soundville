using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;
using Soundville.Presentation.Models.Songs;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Presentation.Services
{
    public class SongPresentationService : ISongPresentationService
    {
        private readonly IStationSongDomainService _stationSongDomainService;
        private readonly ISongDomainService _songDomainService;

        public SongPresentationService(ISongDomainService songDomainService, IStationSongDomainService stationSongDomainService)
        {
            _songDomainService = songDomainService;
            _stationSongDomainService = stationSongDomainService;
        }

        public void Save(SongSaveModel model)
        {
            var saveSong = new Song();
            
            saveSong.Artist = model.Artist;
            saveSong.Title = model.Title;
            _songDomainService.Save(saveSong);

            var song = _songDomainService.GetByArtistAndTitle(model.Artist, model.Title);

            var stationSong = new StationSong();
            stationSong.StationId = model.StationId;
            stationSong.Duration = model.Duration;
            stationSong.SongId = song.Id;
            stationSong.FileName = model.FileName;
            stationSong.SongUrl = model.SongUrl;

            var lastSongId = _stationSongDomainService.GetLastSongPosition(model.StationId);
            stationSong.Position = lastSongId + 1;

            _stationSongDomainService.Save(stationSong);
        }
    }
}
