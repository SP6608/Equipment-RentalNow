using Equipment_RentalNow.Data.Models;
using Equipment_RentalNow.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Equipment_RentalNow.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EquipmentItem>().HasData(
    new EquipmentItem
    {
        Id = 1,
        Name = "Лаптоп Dell",
        Description = "Dell Latitude 5420, i5, 16GB RAM",
        AvailableQuantity = 5,
        Condition = "Ново",
        ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=500"
    },
    new EquipmentItem
    {
        Id = 2,
        Name = "Проектор Epson",
        Description = "HD проектор за презентации",
        AvailableQuantity = 3,
        Condition = "Използвано",
        ImageUrl = "https://images.unsplash.com/photo-1581093458791-9f3c3b7d7b3f?w=500"
    },
    new EquipmentItem
    {
        Id = 3,
        Name = "Камера Canon",
        Description = "DSLR камера за снимки и видео",
        AvailableQuantity = 2,
        Condition = "Ново",
        ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=500"
    },
    new EquipmentItem
    {
        Id = 4,
        Name = "Микрофон Rode",
        Description = "Студиен микрофон",
        AvailableQuantity = 4,
        Condition = "Ново",
        ImageUrl = "https://images.unsplash.com/photo-1511379938547-c1f69419868d?w=500"
    },
    new EquipmentItem
    {
        Id = 5,
        Name = "3D Принтер Creality",
        Description = "Creality Ender 3 V2",
        AvailableQuantity = 1,
        Condition = "Използвано",
        ImageUrl = "https://images.unsplash.com/photo-1581091870627-3b5c1c1f9d4b?w=500"
    }
);


            builder.Entity<RentalRequestItem>()
            .HasKey(x => new { x.RentalRequestId, x.EquipmentItemId });
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public virtual DbSet<EquipmentItem> EquipmentItems { get; set; } = null!;

        public virtual DbSet<RentalRequest> RentalRequests { get; set; } = null!;

        public virtual DbSet<RentalRequestItem> RentalRequestItems { get; set; } = null!;
    }
}
