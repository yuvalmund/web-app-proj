using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RentAHouse.Roles
{
    public static class RolesData
    {
        private static readonly string[] roles = new[]
        {
            "Member",
            "Admin"
        };

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var create = await roleManager.CreateAsync(new IdentityRole(role));

                    if (!create.Succeeded)
                    {
                        throw new Exception("Faiiled to create role");
                    }
                }
            }
        }
    }
}
