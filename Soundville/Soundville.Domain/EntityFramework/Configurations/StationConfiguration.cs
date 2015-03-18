using System.Data.Entity.ModelConfiguration;
using Soundville.Domain.Models;

namespace Soundville.Domain.EntityFramework.Configurations
{
    public class StationConfiguration : EntityTypeConfiguration<Station>
    {
        public StationConfiguration()
        {
            HasKey(x => x.Id);

            HasMany(x => x.StationSongs)
                .WithRequired(x => x.Station)
                .HasForeignKey(x => x.StationId);
        }
    }
}
