using Microsoft.EntityFrameworkCore;
using WorkshopApi.Models;

namespace WorkshopApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        DbSet<Movie> Movies { get; set; }
    }
}
