using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Data.Models
{
    public class User : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }

        object IIdentifiable.Id => Id;

    }
}
