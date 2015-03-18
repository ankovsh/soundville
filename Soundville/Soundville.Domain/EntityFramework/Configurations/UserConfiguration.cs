using System.Data.Entity.ModelConfiguration;
using Soundville.Domain.Models;

namespace Soundville.Domain.EntityFramework.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(x => x.Id);

            HasMany(x => x.Stations)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
