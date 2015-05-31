using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Models
{
    public class Vote : IBaseDomainModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int StationSongId { get; set; }
        public virtual StationSong StationSong { get; set; }
        public int Value { get; set; }

        public Vote()
        {
        }

        public Vote(int userId, int stationSongId, int value)
        {
            UserId = userId;
            StationSongId = stationSongId;
            Value = value;
        }
    }
}
