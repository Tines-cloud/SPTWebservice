using Microsoft.EntityFrameworkCore;
using SPTUserService.Models;

namespace SPTUserService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<UserPreference> UserPreference { get; set; }

        public DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=HADES\\SQLEXPRESS;Database=SPT_USER_DB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One Relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserPreference)
                .WithOne(up => up.User)
                .HasForeignKey<UserPreference>(up => up.Username);

            // One-to-Many Relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Unique constraint
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();
        }
    }
}
