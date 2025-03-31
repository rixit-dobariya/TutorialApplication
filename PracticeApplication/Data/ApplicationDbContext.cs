using Microsoft.EntityFrameworkCore;
using PracticeApplication.Models;

namespace PracticeApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
