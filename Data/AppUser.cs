using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Equipment_RentalNow.Data
{
    public class AppUser:IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength (50)]
        public string LastName { get; set; } = null!;
    }
}
