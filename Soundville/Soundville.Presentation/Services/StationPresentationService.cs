using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;
using Soundville.Presentation.Models.Stations;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Presentation.Services
{
    public class StationPresentationService : IStationPresentationService
    {
        private readonly IStationDomainService _stationDomainService;
        private readonly IUserDomainService _userDomainService;

        public StationPresentationService(IStationDomainService stationDomainService, IUserDomainService userDomainService)
        {
            _stationDomainService = stationDomainService;
            _userDomainService = userDomainService;
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
            
            _stationDomainService.Save(station);
        }
    }
}
