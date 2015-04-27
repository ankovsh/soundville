using System.Collections.Generic;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;
using Soundville.Infrastructure.Constants;
using Soundville.Presentation.Models.Stations;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Presentation.Services
{
    public class StationPresentationService : IStationPresentationService
    {
        private readonly IStationDomainService _stationDomainService;
        private readonly IUserDomainService _userDomainService;
        private readonly IStationSongDomainService _stationSongDomainService;
        private readonly ISongDomainService _songDomainService;

        public StationPresentationService(IStationDomainService stationDomainService, IUserDomainService userDomainService, IStationSongDomainService stationSongDomainService, ISongDomainService songDomainService)
        {
            _stationDomainService = stationDomainService;
            _userDomainService = userDomainService;
            _stationSongDomainService = stationSongDomainService;
            _songDomainService = songDomainService;
        }

        public StationEditModel GetStationEditModel(int? id)
        {
            var station = id.HasValue
                ? _stationDomainService.GetStationById(id.Value)
                : new Station();

            var model = new StationEditModel(station);
            return model;
        }

        public void Save(StationEditModel model, string newImageName, string userEmail)
        {
            var station = model.Id.HasValue
                ?_stationDomainService.GetStationById(model.Id.Value)
                : new Station();

            station.Name = model.Name;
            station.ImageFileName = newImageName;
            station.UserId = _userDomainService.GetByEmail(userEmail).Id;
            station.Status = model.Status;
            
            _stationDomainService.Save(station);
        }

        public StationListModel GetMyStationsModel(string userEmail)
        {
            var stations = _stationDomainService.GetAllStationsByUser(userEmail);
            var model = new StationListModel(stations);

            return model;
        }

        public ViewStationModel GetViewStationModel(int stationId)
        {
            var station = _stationDomainService.GetStationById(stationId);
            var stationItem = new StationItem(station);

            var stationSongs = _stationSongDomainService.GetAllStationSongByStation(station.Id);
            var stationSongItems = new List<StationSongItem>();
            foreach (var stationSong in stationSongs)
            {
                stationSong.Song = _songDomainService.GetSongById(stationSong.SongId);
                stationSongItems.Add(new StationSongItem(stationSong));
            }

            var model = new ViewStationModel(stationItem, stationSongItems);
            return model;
        }

        public MySearchStationsModel GetSearchStationsModel()
        {
            var stations = _stationDomainService.GetAllStations();
            var model = new MySearchStationsModel(stations);

            return model;
        }

        public MySearchStationsModel GetSearchStationsModelByName(string name) 
        {
            var stations = _stationDomainService.GetStationsByName(name);
            var model = new MySearchStationsModel(stations);

            return model;
        }

        public bool IsOwner(int stationId, string userEmail)
        {
            return _stationDomainService.IsOwner(stationId, userEmail);
        }

        public void SaveSubscriber(int stationId, string userEmail)
        {
            var user = _userDomainService.GetByEmail(userEmail);
            var station = _stationDomainService.GetStationById(stationId);
            station.Subscribers.Add(user);
            _stationDomainService.Save(station);
        }

        public StationListModel GetSignedStationsModel(string userEmail)
        {
            var stations = _stationDomainService.GetSignedStationsBySubscriber(userEmail);
            var model = new StationListModel(stations);

            return model;
        }

        public void SaveStationStatus(int stationId, StationStatus status)
        {
            var station = _stationDomainService.GetStationById(stationId);
            station.Status = status;

            _stationDomainService.Save(station);
        }
    }
}
