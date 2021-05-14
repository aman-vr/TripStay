using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Persistence
{
    public class DataSeedingInitializer
    {
        public static async Task UserAndRoleSeedAsync(UserManager<IdentityUser> userManager,
                                                 RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Manager", "Staff" };
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Create Manager User
            if (userManager.FindByEmailAsync("manager.ts@gmail.com").Result == null)
            {
                IdentityUser user = new()
                {
                    UserName = "manager.ts@gmail.com",
                    Email = "manager.ts@gmail.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "TS@manager9").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            //Create Staff User
            if (userManager.FindByEmailAsync("staff.ts@gmail.com").Result == null)
            {
                IdentityUser user = new()
                {
                    UserName = "staff.ts@gmail.com",
                    Email = "staff.ts@gmail.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "TS@staff0").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }
            }

            //Create No Role User
            if (userManager.FindByEmailAsync("amanv7401@gmail.com").Result == null)
            {
                IdentityUser user = new()
                {
                    UserName = "amanv7401@gmail.com",
                    Email = "amanv7401@gmail.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Chitkara@512").Result;
            }
        }
    }
}
