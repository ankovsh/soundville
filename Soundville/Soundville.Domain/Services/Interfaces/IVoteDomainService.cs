using Soundville.Domain.Models;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IVoteDomainService : IDomainService<Vote>
    {
        bool IsAlreadyVoted(int stationSongId, int userId);
        Vote GetVoteBySongId(int songId, string email);
    }
}
