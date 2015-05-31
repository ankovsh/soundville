using System.Data.Entity.ModelConfiguration;
using Soundville.Domain.Models;

namespace Soundville.Domain.EntityFramework.Configurations
{
    public class VoteConfiguration : EntityTypeConfiguration<Vote>
    {
        public VoteConfiguration()
        {
            HasKey(x => x.Id);

            HasRequired(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            HasRequired(x => x.StationSong)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.StationSongId);
        }
    }
}
