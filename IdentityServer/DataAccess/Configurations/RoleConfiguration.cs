using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "eb4f2b9e-de5f-4f57-8cf3-2bdc18fd0bf4"
            },
            new IdentityRole
            {
                Name = "Visitor",
                NormalizedName = "VISITOR",
                Id = "bd09602d-d423-4c59-a64d-4309cad4fd39"
            });
        }
    }
}