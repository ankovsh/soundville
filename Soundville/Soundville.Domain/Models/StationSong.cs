using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Models
{
    public class StationSong : IBaseDomainModel
    {
        public int Id { get; set; }
        public Song Song { get; set; }
        public Station Station { get; set; }
        public string SongUrl { get; set; }
        public string FileName { get; set; }
        public int Duration { get; set; }
    }
}
