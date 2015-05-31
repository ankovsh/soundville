using System;
using System.Linq;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;
using Soundville.Infrastructure.Constants;
using Soundville.Presentation.Models.Songs;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Presentation.Services
{
    public class SongPresentationService : ISongPresentationService
    {
        private readonly IStationSongDomainService _stationSongDomainService;
        private readonly ISongDomainService _songDomainService;
        private readonly IVoteDomainService _voteDomainService;
        private readonly IUserDomainService _userDomainService;

        public SongPresentationService(
            ISongDomainService songDomainService,
            IStationSongDomainService stationSongDomainService,
            IVoteDomainService voteDomainService,
            IUserDomainService userDomainService)
        {
            _songDomainService = songDomainService;
            _stationSongDomainService = stationSongDomainService;
            _voteDomainService = voteDomainService;
            _userDomainService = userDomainService;
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

        public string Vote(int stationSongId, int value, string email)
        {
            bool isExist = _stationSongDomainService.IsExist(stationSongId);
            if (!isExist)
            {
                return "Song is not found.";
            }

            if (value != -1 && value != 1)
            {
                throw new ArgumentException("Value of a vote can be -1 or 1.");
            }

            var user = _userDomainService.GetByEmail(email);

            bool isAlreadyVoted = _voteDomainService.IsAlreadyVoted(stationSongId, user.Id);
            if (isAlreadyVoted)
            {
                return "You have already voted the song.";
            }

            var stationSong = _stationSongDomainService.GetById(stationSongId);
            if (stationSong.Station.Status != StationStatus.Created)
            {
                return "The station has already played.";
            }

            var vote = new Vote(user.Id, stationSongId, value);
            _voteDomainService.Create(vote);
            
            var stationSongs = _stationSongDomainService.GetAllStationSongByStation(stationSong.StationId);

            stationSongs = stationSongs.OrderByDescending(x => x.Votes.Sum(v => v.Value)).ToList();
            for (int i = 0; i < stationSongs.Count; i++)
            {
                stationSongs[i].Position = i + 1;
            }

            _stationSongDomainService.SaveChanges();

            return null;
        }
    }
}
