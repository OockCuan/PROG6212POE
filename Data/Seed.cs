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
            context.Database.Migrate();

            string[] roles = { "HR", "Lecturer", "AcademicManager", "ProgramCoordinator" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

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
                await userManager.CreateAsync(hrUser, "@1passForLogin");
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

            
            var hrProfile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == hrUser.Id);
            if (hrProfile == null)
            {
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

                await userManager.CreateAsync(amUser, "@1passForLogin");
                await userManager.AddToRoleAsync(amUser, "AcademicManager");

                context.Profiles.Add(new Profiles
                {
                    UserId = amUser.Id,
                    Name = "Academic",
                    Surname = "Manager",
                    Department = "Admin",
                    DefaultRatePerJob = 0,
                    RoleName = "AcademicManager"
                });

                await context.SaveChangesAsync();
            }

            var amProfile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == amUser.Id);
            if (amProfile == null)
            {
                context.Profiles.Add(new Profiles
                {
                    UserId = amUser.Id,
                    Name = "Academic",
                    Surname = "Manager",
                    Department = "Admin",
                    DefaultRatePerJob = 0,
                    RoleName = "AcademicManager"
                });
                await context.SaveChangesAsync();
            }

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

                await userManager.CreateAsync(pcUser, "@1passForLogin");
                await userManager.AddToRoleAsync(pcUser, "ProgramCoordinator");

                context.Profiles.Add(new Profiles
                {
                    UserId = pcUser.Id,
                    Name = "Alice",
                    Surname = "Coord",
                    DefaultRatePerJob = 0,
                    Department = "Project Mangemnet",
                    RoleName = "ProgramCoordinator"
                });

                await context.SaveChangesAsync();
            }

            var pcProfile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == pcUser.Id);
            if (pcProfile == null)
            {
                context.Profiles.Add(new Profiles
                {
                    UserId = pcUser.Id,
                    Name = "Alice",
                    Surname = "Coord",
                    DefaultRatePerJob = 0,
                    Department = "Project Mangemnet",
                    RoleName = "ProgramCoordinator"
                });
                await context.SaveChangesAsync();
            }

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

                await userManager.CreateAsync(lectUser, "@1passForLogin");
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

            var lectProfile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == lectUser.Id);
            if (lectProfile == null)
            {
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

            var kyleEmail = "kyle@email.com";
            var kyleUser = await userManager.FindByEmailAsync(kyleEmail);
            if (kyleUser == null)
            {
                kyleUser = new IdentityUser
                {
                    UserName = kyleEmail,
                    Email = kyleEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(kyleUser, "@1passForLogin");
                await userManager.AddToRoleAsync(kyleUser, "Lecturer");

                context.Profiles.Add(new Profiles
                {
                    UserId = kyleUser.Id,
                    Name = "Kyle",
                    Surname = "Example",
                    DefaultRatePerJob = 100,
                    Department = "Education",
                    RoleName = "Lecturer"
                });

                await context.SaveChangesAsync();
            }

            var kyleProfile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == kyleUser.Id);
            if (kyleProfile == null)
            {
                context.Profiles.Add(new Profiles
                {
                    UserId = kyleUser.Id,
                    Name = "Kyle",
                    Surname = "Example",
                    DefaultRatePerJob = 100,
                    Department = "Education",
                    RoleName = "Lecturer"
                });
                await context.SaveChangesAsync();
            }


        }

    }
}
