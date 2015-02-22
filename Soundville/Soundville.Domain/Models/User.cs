using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Models
{
    public class User : IBaseDomainModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string PasswordHash { get; set; }
    }
}
