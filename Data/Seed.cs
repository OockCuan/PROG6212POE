using System;
using System.Threading.Tasks;
using PROGPOEst10439216.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace PROGPOEst10439216.Data
{
    public class Seed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            //migrating on launch of application to ensure all data is present
            context.Database.Migrate();
            //array containing all the possible roles for a worker, and what the authorization is based on
            string[] roles = { "HR", "Lecturer", "AcademicManager", "ProgramCoordinator" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //information for hr role
            var hrEmail = "hr@email.com";
            var hrUser = await userManager.FindByEmailAsync(hrEmail);
            if (hrUser == null)
            {
                hrUser = new IdentityUser
                {
                    UserName = hrEmail,
                    Email = hrEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(hrUser, "password");
                await userManager.AddToRoleAsync(hrUser, "HR");

                context.Profiles.Add(new Profiles
                {
                    UserId = hrUser.Id,
                    Name = "HR",
                    Surname = "Example",
                    Department = "Admin",
                    DefaultRatePerJob = 0,
                    RoleName = "HR"
                });

                await context.SaveChangesAsync();
            }

            //information for academic manager role
            var amEmail = "manager@email.com";
            var amUser = await userManager.FindByEmailAsync(amEmail);
            if (amUser == null)
            {
                amUser = new IdentityUser
                {
                    UserName = amEmail,
                    Email = amEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(amUser, "password");
                await userManager.AddToRoleAsync(amUser, "AcademicManager");

                context.Profiles.Add(new Profiles
                {
                    UserId = amUser.Id,
                    Name = "Academic",
                    Surname = "Manager",
                    Department = "N/A",
                    DefaultRatePerJob = 0,
                    RoleName = "AcademicManager"
                });

                await context.SaveChangesAsync();
            }

            //information for project coordinator role
            var pcEmail = "coordinator@email.com";
            var pcUser = await userManager.FindByEmailAsync(pcEmail);
            if (pcUser == null)
            {
                pcUser = new IdentityUser
                {
                    UserName = pcEmail,
                    Email = pcEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(pcUser, "password");
                await userManager.AddToRoleAsync(pcUser, "ProgramCoordinator");

                context.Profiles.Add(new Profiles
                {
                    UserId = pcUser.Id,
                    Name = "Construction",
                    Surname = "Manager",
                    DefaultRatePerJob = 0,
                    Department = "Construction",
                    RoleName = "ConstructionManager"
                });

                await context.SaveChangesAsync();
            }

            //information for lecturer role 
            var lectEmail = "lecturer@email.com";
            var lectUser = await userManager.FindByEmailAsync(lectEmail);
            if (lectUser == null)
            {
                lectUser = new IdentityUser
                {
                    UserName = lectEmail,
                    Email = lectEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(lectUser, "password");
                await userManager.AddToRoleAsync(lectUser, "Lecturer");

                context.Profiles.Add(new Profiles
                {
                    UserId = lectUser.Id,
                    Name = "Kevin",
                    Surname = "Lecturer",
                    DefaultRatePerJob = 150,
                    Department = "Programming",
                    RoleName = "Lecturer"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
