using Microsoft.AspNetCore.Identity;
using ReadingDiary.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Infrastructure.Seed
{

    /// <summary>
    /// Seeds required Identity roles and a default administrator account.
    /// 
    /// Roles are part of the core application logic.
    /// The administrator account is intended for development and demo purposes only.
    /// </summary>
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {

            // Roles required by application logic
            // Librarian role is reserved for a future feature
            string[] roles = { "Admin", "Librarian", "Reader" };

            
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Default administrator account (DEV / DEMO only)
            string adminEmail = "admin@admin.com";
            string adminPassword = "Admin123!";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    DisplayName = "Admin"
                };

                var result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
