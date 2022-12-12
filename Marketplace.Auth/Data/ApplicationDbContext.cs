using IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(e => e.Name)
                .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(e => e.Surname)
                .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(e => e.Country)
                .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(e => e.Street)
                .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(e => e.Region)
                .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(e => e.City)
                .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(e => e.PostalCode)
                .HasMaxLength(5);
        }
    }
}