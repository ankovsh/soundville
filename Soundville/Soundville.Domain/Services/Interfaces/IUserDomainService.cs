using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IUserDomainService : IDomainService<User>
    {
        User GetByUserName(string userName);
    }
}
