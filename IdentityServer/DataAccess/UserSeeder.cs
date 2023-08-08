using System.Security.Claims;
using IdentityModel;
using IdentityServer.Entities;
using IdentityServer.Models.Enums;
using Microsoft.AspNetCore.Identity;
using ILogger = Serilog.ILogger;

namespace IdentityServer.DataAccess
{
    public class UserSeeder
    {
        public async static void Seed(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                await CreateUser(scope, "Sharthak", "Mallik", "123 Siliguri, West bengal, 734003", "P@ssword", UserRoles.Admin, "sharthakmallik@emal.com");
                await CreateUser(scope, "Dipa", "Das", "123 Siliguri, West bengal, 734003", "P@ssword", UserRoles.Visitor, "dipad@emal.com");
            }
        }

        private async static Task CreateUser(
            IServiceScope scope,
            string fisrtName,
            string lastName,
            string address,
            string password,
            UserRoles role,
            string email
        )
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var isUserAvailable = await userManager.FindByNameAsync(email) != null;
            if (!isUserAvailable)
            {
                var user = new User
                {
                    FirstName = fisrtName,
                    LastName = lastName,
                    UserName = email,
                    Email = email,
                    Address = address
                };
                var creationResult = await userManager.CreateAsync(user, password);
                CheckResult(creationResult);

                var roleResult = await userManager.AddToRoleAsync(user, role.ToString());
                CheckResult(roleResult);

                var claimsResult = await userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(JwtClaimTypes.GivenName, user.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, user.LastName),
                    new Claim(JwtClaimTypes.Address, user.Address),
                    new Claim(JwtClaimTypes.Role, role.ToString()),
                    new Claim(JwtClaimTypes.Email, user.Email)
                });
                CheckResult(claimsResult);
            }
            else
            {
                logger.Error("User {@email} already exist", email);
            }
        }

        private static void CheckResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new Exception();
            }
        }
    }
}