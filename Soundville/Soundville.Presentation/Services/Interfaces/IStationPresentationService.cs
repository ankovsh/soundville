using Soundville.Presentation.Models.Stations;

namespace Soundville.Presentation.Services.Interfaces
{
    public interface IStationPresentationService : IPresentationService
    {
        void Save(StationEditModel model, string newImageName, string userEmail);
        StationEditModel GetStationEditModel(int? id);
        MyStationsModel GetMyStationsModel(string userEmail);
    }
}
