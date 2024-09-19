using Microsoft.EntityFrameworkCore;
using SPTKnowledgeService.Models;

namespace SPTKnowledgeService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Knowledge> Knowledge { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<StudySession> StudySession { get; set; }
        public DbSet<Break> Breaks { get; set; }
        public DbSet<BreakDuration> BreakDuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=HADES\\SQLEXPRESS;Database=SPT_KNOWLEDGE_DB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudySession>()
                .HasOne(ss => ss.Break)
                .WithOne(b => b.StudySession)
                .HasForeignKey<Break>(b => b.StudySessionId);

            modelBuilder.Entity<Break>()
                .HasMany(b => b.BreakDurations)
                .WithOne(bd => bd.Break)
                .HasForeignKey(bd => bd.BreakId);
        }

    }
}
