using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsReady.Models;
using Microsoft.EntityFrameworkCore.Metadata; // Add this using directive

namespace OpsReady.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }    // <-- add this

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<User>()
        //        .HasIndex(u => u.Username)
        //        .IsUnique();

        //    modelBuilder.Entity<UserProfile>()                 // optional: explicit config
        //        .HasKey(up => up.UserId);
        //}
    }
}
