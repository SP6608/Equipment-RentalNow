using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Equipment_RentalNow.Common
{
    public class RolesSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private readonly ICollection<IdentityRole>Roles
            =new HashSet<IdentityRole> ()
            {
                new IdentityRole{Id="58f6feec-f0c4-4e1c-af3e-98e3b5f900d8",Name="Admin",NormalizedName="ADMIN",ConcurrencyStamp="58f6feec-f0c4-4e1c-af3e-98e3b5f900d8"},
                new IdentityRole{Id="55177ecf-7d5e-494e-b400-80c5dc14f8ca",Name="User",NormalizedName="USER",ConcurrencyStamp="55177ecf-7d5e-494e-b400-80c5dc14f8ca"}
            };
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(Roles);   
        }
    }
}
