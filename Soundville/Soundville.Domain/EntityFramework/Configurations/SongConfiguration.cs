using System.Data.Entity.ModelConfiguration;
using Soundville.Domain.Models;

namespace Soundville.Domain.EntityFramework.Configurations
{
    public class SongConfiguration : EntityTypeConfiguration<Song>
    {
        public SongConfiguration()
        {
            HasKey(x => x.Id);
        }
    }
}
