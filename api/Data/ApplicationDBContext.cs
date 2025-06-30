using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Portfolio>()
                .HasKey(p => new { p.AppUserId, p.StockId });
            builder.Entity<Portfolio>()
                .HasOne(p => p.AppUser).WithMany(u => u.portfolios)
                .HasForeignKey(p => p.AppUserId);
            
            builder.Entity<Portfolio>()
                .HasOne(p => p.Stock).WithMany(u => u.portfolios)
                .HasForeignKey(p => p.StockId);


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
            {
                Id = "1a1a1a1a-aaaa-1111-aaaa-111111111111",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2b2b2b2b-bbbb-2222-bbbb-222222222222",
                Name = "User",
                NormalizedName = "USER"
            },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}