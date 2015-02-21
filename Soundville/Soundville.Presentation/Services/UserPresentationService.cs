using System.Linq;
using Soundville.Domain.Services.Interfaces;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Presentation.Services
{
    public class UserPresentationService : IUserPresentationService
    {
        private readonly IUserDomainService _userDomainService;

        public UserPresentationService(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public string GetFirstUserName()
        {
            var user = _userDomainService.GetAll().FirstOrDefault();
            if (user == null)
            {
                return "empty";
            }

            return user.UserName;
        }
    }
}
