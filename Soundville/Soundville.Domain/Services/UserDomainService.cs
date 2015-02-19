using System.Linq;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class UserDomainService : DomainService<User>, IUserDomainService
    {
        public UserDomainService(/*ISoundvilleContext soundvilleContext*/)
            : base(/*soundvilleContext*/ new SoundvilleContext())
        {
        }

        public User GetByUserName(string userName)
        {
            return Context.Users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
