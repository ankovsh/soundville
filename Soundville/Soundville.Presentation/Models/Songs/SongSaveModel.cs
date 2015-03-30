namespace Soundville.Presentation.Models.Songs
{
    public class SongSaveModel
    {
        public int StationId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string SongUrl { get; set; }
        public string FileName { get; set; }
        public int Duration { get; set; }
    }
}
