using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;
using Soundville.Presentation.Models.Profiles;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Presentation.Services
{
    public class ProfilePresentationService : IProfilePresentationService
    {
        private readonly IUserDomainService _userDomainService;

        public ProfilePresentationService(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public void Save(ProfileEditModel model, string oldEmail, string newImageName)
        {
            User user = _userDomainService.GetByEmail(oldEmail);
            user.Name = model.Name;
            user.Email = model.Email;
            if (newImageName != null)
            {
                user.ImageFileName = newImageName;
            }
            
            _userDomainService.Save(user);
        }

        public ProfileEditModel GetProfileEditModel(string email)
        {
            User user = _userDomainService.GetByEmail(email);
            var model = new ProfileEditModel(user);
            return model;
        }

        public int GetUserIdByEmail(string email)
        {
            User user = _userDomainService.GetByEmail(email);
            return user.Id;
        }

        public void SaveVkModel(ProfileVkModel model, string email)
        {
            User user = _userDomainService.GetByEmail(email);
            user.Token = model.Token;

            _userDomainService.Save(user);
        }
    }
}