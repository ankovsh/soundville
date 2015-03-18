using System.Data.Entity.ModelConfiguration;
using Soundville.Domain.Models;

namespace Soundville.Domain.EntityFramework.Configurations
{
    public class StationSongConfiguration : EntityTypeConfiguration<StationSong>
    {
        public StationSongConfiguration()
        {
            HasKey(x => x.Id);

            HasRequired(x => x.Song)
                .WithMany()
                .HasForeignKey(x => x.SongId);
        }
    }
}
