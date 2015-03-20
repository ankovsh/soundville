using Soundville.Domain.Models;

namespace Soundville.Presentation.Models.Stations
{
    public class SongItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }

        public SongItem(Song song)
        {
            Id = song.Id;
            Title = song.Title;
            Artist = song.Artist;
        }
    }
}
