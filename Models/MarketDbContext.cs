using Microsoft.EntityFrameworkCore;

namespace MarketApi.Models
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "حليب كامل الدسم 1 لتر",
                    Category = "ألبان",
                    Price = 45.00m,
                    ImageUrl = "https://images.unsplash.com/photo-1563636619-e9143da7973b?w=300"
                },
                new Product
                {
                    Id = 2,
                    Name = "جبنة بيضاء 500 جرام",
                    Category = "ألبان",
                    Price = 65.00m,
                    ImageUrl = "https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=300"
                },
                new Product
                {
                    Id = 3,
                    Name = "زيت عباد الشمس 1 لتر",
                    Category = "زيوت",
                    Price = 90.00m,
                    ImageUrl = "https://images.unsplash.com/photo-1474979266404-7eaacbcd87c5?w=300"
                },
                new Product
                {
                    Id = 4,
                    Name = "أرز مصري فاخر 1 كجم",
                    Category = "بقوليات",
                    Price = 35.00m,
                    ImageUrl = "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300"
                },
                new Product
                {
                    Id = 5,
                    Name = "مكرونة فرن 400 جرام",
                    Category = "بقوليات",
                    Price = 18.00m,
                    ImageUrl = "https://images.unsplash.com/photo-1621996346565-e3d5d6281691?w=300"
                }
            );
        }
    }
}