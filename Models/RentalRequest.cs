using Equipment_RentalNow.Data;
using Equipment_RentalNow.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Equipment_RentalNow.Data.Models
{
    public class RentalRequest
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending";

        
        [Required]
        public string UserId { get; set; } = null!;

        public AppUser User { get; set; } = null!;

        
        public ICollection<RentalRequestItem> RentalRequestItems { get; set; }
            = new HashSet<RentalRequestItem>();
    }
}
