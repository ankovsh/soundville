using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IUserDomainService : IDomainService<User>
    {
        User GetByEmail(string email);
        void Save(User user);
    }
}
