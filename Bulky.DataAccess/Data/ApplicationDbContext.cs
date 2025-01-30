using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TutorialApplication.Models;

namespace TutorialApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> //here IdentityUser default user is optional 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Action" ,DisplayOrder=1},
                new Category { Id=2, Name="Romance" ,DisplayOrder=3},
                new Category { Id=3, Name="SciFi" ,DisplayOrder=2}
            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Blue Lock",
                    Author = "Ego Jenpachi",
                    CategoryId = 2,
                    Description = "Hello World",
                    ISBN = "123456789",
                    ListPrice = 100,
                    Price = 90,
                    Price50 = 80,
                    Price100 = 70,
                    ImageUrl="123",
                },
                new Product
                {
                    Id = 2,
                    Title = "Attack on Titan",
                    Author = "Hajime Isayama",
                    CategoryId = 2,
                    Description = "Fight for freedom.",
                    ISBN = "987654321",
                    ListPrice = 150,
                    Price = 140,
                    Price50 = 130,
                    Price100 = 120,
                    ImageUrl = "123"
                },
                new Product
                {
                    Id = 3,
                    Title = "One Piece",
                    Author = "Eiichiro Oda",
                    CategoryId = 5,
                    Description = "Adventure of Luffy and crew.",
                    ISBN = "1122334455",
                    ListPrice = 200,
                    Price = 180,
                    Price50 = 160,
                    Price100 = 150,
                    ImageUrl = "123"
                },
                new Product
                {
                    Id = 4,
                    Title = "Demon Slayer",
                    Author = "Koyoharu Gotouge",
                    CategoryId = 5,
                    Description = "Story of Tanjiro Kamado.",
                    ISBN = "2233445566",
                    ListPrice = 120,
                    Price = 110,
                    Price50 = 100,
                    Price100 = 90,
                    ImageUrl = "123"
                },
                new Product
                {
                    Id = 5,
                    Title = "Naruto",
                    Author = "Masashi Kishimoto",
                    CategoryId = 6,
                    Description = "The journey of a ninja.",
                    ISBN = "3344556677",
                    ListPrice = 130,
                    Price = 120,
                    Price50 = 110,
                    Price100 = 100,
                    ImageUrl = "123"
                }
            );
        }
    }
}
