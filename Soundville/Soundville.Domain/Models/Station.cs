using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Models
{
    public class Station : IBaseDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public int UserId { get; set; }
    }
}
