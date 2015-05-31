using System.Collections.Generic;
using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Models
{
    public class StationSong : IBaseDomainModel
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public virtual Song Song { get; set; }
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
        public string SongUrl { get; set; }
        public string FileName { get; set; }
        public int Duration { get; set; }
        public int Position { get; set; }
        public virtual IList<Vote> Votes { get; set; }
    }
}
