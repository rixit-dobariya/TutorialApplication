using CollegeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeWork.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
