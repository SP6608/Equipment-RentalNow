using System.ComponentModel.DataAnnotations;

namespace Equipment_RentalNow.Models
{
    public class EquipmentItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = null!;

        [Range(0, int.MaxValue)]
        public int AvailableQuantity { get; set; }

        [MaxLength(2048)]
        public string? ImageUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Condition { get; set; } = null!;

        public ICollection<RentalRequestItem> RentalRequestItems { get; set; }
            = new HashSet<RentalRequestItem>();
    }
}
