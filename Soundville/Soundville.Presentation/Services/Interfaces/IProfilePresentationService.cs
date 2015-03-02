using Soundville.Presentation.Models.Profiles;

namespace Soundville.Presentation.Services.Interfaces
{
    public interface IProfilePresentationService : IPresentationService
    {
        void Save(ProfileEditModel model, string oldEmail, string newImageName);
        ProfileEditModel GetProfileEditModel(string email);
    }
}
