using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsReady.Models;
using Microsoft.EntityFrameworkCore.Metadata; // Add this using directive

namespace OpsReady.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<TrainingEvent> TrainingEvents { get; set; }    
        public DbSet<TrainingRecord> TrainingRecords { get; set; }
        public DbSet<TrainingAssignment> TrainingAssignments{ get; set; } // <-- add this



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
