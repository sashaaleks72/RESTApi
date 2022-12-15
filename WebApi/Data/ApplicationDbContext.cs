using Microsoft.EntityFrameworkCore;
using Net7.WebApi.Test.Data.Entities;

namespace Net7.WebApi.Test.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TeapotEntity> Teapots { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            // Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var teapots = new List<TeapotEntity>
            {
                new TeapotEntity
                {
                    Id = 1,
                    Title = "Teapot 1",
                    Quantity = 6,
                    Price = 490,
                    Description = "Some good teapot",
                    Capacity = 1.7,
                    WarrantyInMonths = 12,
                    ManufacturerCountry = "Germany",
                    ImgUrl = "image1.png"
                },
                new TeapotEntity
                {
                    Id = 2,
                    Title = "Teapot 2",
                    Quantity = 10,
                    Price = 515,
                    Description = "Some good teapot",
                    Capacity = 1.8,
                    WarrantyInMonths = 9,
                    ManufacturerCountry = "China",
                    ImgUrl = "image2.png"
                }
            };

            modelBuilder.Entity<TeapotEntity>().HasData(teapots);
        }
    }
}
