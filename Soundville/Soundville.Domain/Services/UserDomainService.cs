using System.Linq;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class UserDomainService : DomainService<User>, IUserDomainService
    {
        public UserDomainService(ISoundvilleContext soundvilleContext)
            : base(soundvilleContext)
        {
        }

        public User GetByEmail(string email)
        {
            return Context.Users.FirstOrDefault(x => x.Email == email);
        }

        public void Save(User user)
        {
            Context.SaveChanges();
        }
    }
}
