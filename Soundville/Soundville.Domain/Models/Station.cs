using System.Collections.Generic;
using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Models
{
    public class Station : IBaseDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual IList<StationSong> StationSongs { get; set; }
        public virtual IList<User> Subscribers { get; set; }
    }
}
