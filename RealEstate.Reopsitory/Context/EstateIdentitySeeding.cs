using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.Entiry.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Context
{
    public class EstateIdentitySeeding
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "AbdulrahmanSalah",
                    DisplayName = "Abdulrahman Salah",
                    Email = "Abdulrahman7@Gmail.com"
                };

                await userManager.CreateAsync(user, "Passw12345@");
            }
        }
    }
}
