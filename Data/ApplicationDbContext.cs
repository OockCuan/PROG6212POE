using PROGPOEst10439216.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace PROGPOEst10439216.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Claims> Claims { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
    }
}
