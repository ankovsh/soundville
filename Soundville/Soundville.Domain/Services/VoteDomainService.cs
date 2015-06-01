using System.Linq;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class VoteDomainService : DomainService<Vote>, IVoteDomainService
    {
        public VoteDomainService(ISoundvilleContext soundvilleContext) : base(soundvilleContext)
        {
        }

        public bool IsAlreadyVoted(int stationSongId, int userId)
        {
            return Context.Votes.Any(x => x.StationSongId == stationSongId && x.UserId == userId);
        }

        public Vote GetVoteBySongId(int songId, string email)
        {
            return Context.Votes.SingleOrDefault(x => x.User.Email == email 
                                                && x.StationSongId == songId);
        }
    }
}
