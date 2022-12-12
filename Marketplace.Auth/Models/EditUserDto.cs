using Marketplace.Auth.Models;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class EditUserDto
    {
        [StringLength(100, ErrorMessage = ErrorMessages.MIN_MAX_STRING_LENGTH, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [StringLength(100, ErrorMessage = ErrorMessages.MIN_MAX_STRING_LENGTH, MinimumLength = 1)]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [StringLength(100, ErrorMessage = ErrorMessages.MIN_MAX_STRING_LENGTH, MinimumLength = 1)]
        [Display(Name = "Surname")]
        public string? Surname { get; set; }

        [RegularExpression("[0-9]{10}", ErrorMessage = "The {0} must contain 10 digits")]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [StringLength(100, ErrorMessage = ErrorMessages.MIN_MAX_STRING_LENGTH, MinimumLength = 1)]
        [Display(Name = "Country")]
        public string? Country { get; set; }

        [StringLength(100, ErrorMessage = ErrorMessages.MIN_MAX_STRING_LENGTH, MinimumLength = 1)]
        [Display(Name = "Region")]
        public string? Region { get; set; }

        [StringLength(100, ErrorMessage = ErrorMessages.MIN_MAX_STRING_LENGTH, MinimumLength = 1)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [StringLength(100, ErrorMessage = ErrorMessages.MIN_MAX_STRING_LENGTH, MinimumLength = 1)]
        [Display(Name = "Street")]
        public string? Street { get; set; }

        [RegularExpression("[0-9]{5}", ErrorMessage = "The {0} must contain 5 digits")]
        [Display(Name = "Postal Code")]
        public int? PostalCode { get; set; }
    }
}
