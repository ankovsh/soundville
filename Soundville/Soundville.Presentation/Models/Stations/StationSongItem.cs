using Soundville.Domain.Models;

namespace Soundville.Presentation.Models.Stations
{
    public class StationSongItem
    {
        public int Id { get; set; }
        public SongItem Song { get; set; }
        public string SongUrl { get; set; }
        public string FileName { get; set; }
        public int Duration { get; set; }

        public StationSongItem(StationSong stationSong)
        {
            Song = new SongItem(stationSong.Song);
            Id = stationSong.Id;
            SongUrl = stationSong.SongUrl;
            FileName = stationSong.FileName;
            Duration = stationSong.Duration;
        }
    }
}
