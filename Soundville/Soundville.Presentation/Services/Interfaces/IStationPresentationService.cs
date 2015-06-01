using Soundville.Infrastructure.Constants;
using Soundville.Presentation.Models.Stations;

namespace Soundville.Presentation.Services.Interfaces
{
    public interface IStationPresentationService : IPresentationService
    {
        void Save(StationEditModel model, string newImageName, string userEmail);
        StationEditModel GetStationEditModel(int? id);
        StationListModel GetMyStationsModel(string userEmail);
        MySearchStationsModel GetSearchStationsModel();
        MySearchStationsModel GetSearchStationsModelByName(string name);
        ViewStationModel GetViewStationModel(int stationId, string email);
        bool IsOwner(int stationId, string userEmail);
        void SaveSubscriber(int stationId, string userEmail);
        StationListModel GetSignedStationsModel(string userEmail);
        void SaveStationStatus(int stationId, StationStatus status);
    }
}
