using Equipment_RentalNow.Models;
using System.ComponentModel.DataAnnotations;

namespace Equipment_RentalNow.Data.Models
{
    public class RentalRequestItem
    {
        [Required]
        public int RentalRequestId { get; set; }

        public RentalRequest RentalRequest { get; set; } = null!;

        [Required]
        public int EquipmentItemId { get; set; }

        public EquipmentItem EquipmentItem { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}