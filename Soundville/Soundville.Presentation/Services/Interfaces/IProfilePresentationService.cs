﻿using Soundville.Presentation.Models.Profiles;

namespace Soundville.Presentation.Services.Interfaces
{
    public interface IProfilePresentationService : IPresentationService
    {
        void Save(ProfileEditModel model, string oldEmail, string newImageName);
        ProfileEditModel GetProfileEditModel(string email);
        int GetUserIdByEmail(string email);
        void SaveVkModel(ProfileVkModel model, string email);
        string GetTokenByEmail(string email);
    }
}
