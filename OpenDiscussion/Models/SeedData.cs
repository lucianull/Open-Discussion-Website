using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenDiscussion.Data;
using static System.Net.WebRequestMethods;

namespace OpenDiscussion.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Roles.Any())
                {
                    return;
                }
                context.Roles.AddRange(
                new IdentityRole { Id = "0f0190ec-276c-41d7-9398-25c4e1714481", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new IdentityRole { Id = "0f0190ec-276c-41d7-9398-25c4e1714482", Name = "Moderator", NormalizedName = "Moderator".ToUpper() },
                new IdentityRole { Id = "0f0190ec-276c-41d7-9398-25c4e1714483", Name = "User", NormalizedName = "User".ToUpper() }
                );
            var hasher = new PasswordHasher<ApplicationUser>();

            context.Users.AddRange(
            new ApplicationUser
            {
                Id = "88744e5d-7de5-482f-b3e8-e818a5605e15",
                UserName = "admin@test.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@TEST.COM",
                Email = "admin@test.com",
                NormalizedUserName = "ADMIN@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "Admin1!")
            },
            new ApplicationUser
            {
                Id = "88744e5d-7de5-482f-b3e8-e818a5605e16",
                UserName = "moderator@test.com",
                EmailConfirmed = true,
                NormalizedEmail = "MODERATOR@TEST.COM",
                Email = "moderator@test.com",
                NormalizedUserName = "MODERATOR@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "Moderator1!")
            },
             new ApplicationUser
             {
                 Id = "88744e5d-7de5-482f-b3e8-e818a5605e17",
                 UserName = "user@test.com",
                 EmailConfirmed = true,
                 NormalizedEmail = "USER@TEST.COM",
                 Email = "user@test.com",
                 NormalizedUserName = "USER@TEST.COM",
                 PasswordHash = hasher.HashPassword(null, "User1!")
             }
             );
                context.UserRoles.AddRange(
                 new IdentityUserRole<string>
                 {
                     RoleId = "0f0190ec-276c-41d7-9398-25c4e1714481",
                     UserId = "88744e5d-7de5-482f-b3e8-e818a5605e15"
                 },
                new IdentityUserRole<string>
                {
                    RoleId = "0f0190ec-276c-41d7-9398-25c4e1714482",
                    UserId = "88744e5d-7de5-482f-b3e8-e818a5605e16"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "0f0190ec-276c-41d7-9398-25c4e1714483",
                    UserId = "88744e5d-7de5-482f-b3e8-e818a5605e17"
                });

                context.SaveChanges();
            }
        }
    }
}
