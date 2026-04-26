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

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public virtual DbSet<EquipmentItem> EquipmentItems { get; set; } = null!;

        public virtual DbSet<RentalRequest> RentalRequests { get; set; } = null!;

        public virtual DbSet<RentalRequestItem> RentalRequestItems { get; set; } = null!;
    }
}
