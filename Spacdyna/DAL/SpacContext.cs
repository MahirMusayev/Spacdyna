using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spacdyna.Models;

namespace Spacdyna.DAL
{
    public class SpacContext : IdentityDbContext<AppUser>
    {
        public SpacContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("server=CA-R214-PC04\\SQLEXPRESS;database=SpacDb;trusted_connection=true;;TrustServerCertificate=True");
            base.OnConfiguring(options);
        }
    }
}
