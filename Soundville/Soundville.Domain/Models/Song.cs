using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Models
{
    public class Song : IBaseDomainModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
    }
}
