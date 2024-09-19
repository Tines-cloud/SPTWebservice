using Microsoft.EntityFrameworkCore;
using SPTSubjectService.Models;

namespace SPTSubjectService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subject> Subject { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=HADES\\SQLEXPRESS;Database=SPT_SUBJECT_DB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }

    }
}
