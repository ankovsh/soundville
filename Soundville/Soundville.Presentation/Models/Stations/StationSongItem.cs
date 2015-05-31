using System.IO;
using System.Linq;
using Castle.Core.Internal;
using Soundville.Domain.Models;
using Soundville.Infrastructure.Constants;

namespace Soundville.Presentation.Models.Stations
{
    public class StationSongItem
    {
        public int Id { get; set; }
        public SongItem Song { get; set; }
        public string SongUrl { get; set; }
        public string FileName { get; set; }
        public int Duration { get; set; }
        public int VoteSum { get; set; }
        public int Position { get; set; }

        public StationSongItem(StationSong stationSong)
        {
            Song = new SongItem(stationSong.Song);
            Id = stationSong.Id;
            SongUrl = Path.Combine(SongConstants.SongUrl, stationSong.FileName);
            FileName = stationSong.FileName;
            Duration = stationSong.Duration;
            VoteSum = stationSong.Votes.IsNullOrEmpty() ? 0 : stationSong.Votes.Sum(x => x.Value);
            Position = stationSong.Position;
        }
    }
}
