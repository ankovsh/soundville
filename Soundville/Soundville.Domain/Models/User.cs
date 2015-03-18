using System.Collections.Generic;
using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Models
{
    public class User : IBaseDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageFileName { get; set; }
        public string PasswordHash { get; set; }
        public virtual IList<Station> Stations { get; set; }
    }
}
