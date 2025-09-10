using Kyzmat.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kyzmat.DAL
{
    public class AppDbContext : IdentityDbContext<User,Role,Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();

            var user1 = new User
            {
                Id = Guid.NewGuid(),
                UserName = "user1",
                NormalizedUserName = "USER1",
                Email = "user1@gmail.com",
                NormalizedEmail = "USER1@GMAIL.COM",
                EmailConfirmed = true,
                Balance = 8.00m,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            user1.PasswordHash = hasher.HashPassword(user1, "user1");

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                UserName = "user2",
                NormalizedUserName = "USER2",
                Email = "user2@gmail.com",
                NormalizedEmail = "USER2@GMAIL.COM",
                EmailConfirmed = true,
                Balance = 8.00m,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            user2.PasswordHash = hasher.HashPassword(user2, "user2");

            builder.Entity<User>().HasData(user1, user2);
        }
    }
}
