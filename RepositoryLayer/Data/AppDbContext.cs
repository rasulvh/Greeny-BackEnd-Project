using DomainLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace RepositoryLayer.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ProductBasket> ProductBaskets { get; set; }
        public DbSet<ProductWishlist> ProductWishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SubCategory>()
            .HasMany(e => e.Products)
            .WithOne(e => e.SubCategory)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rating>()
            .HasMany(e => e.Reviews)
            .WithOne(e => e.Rating)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
            .HasMany(e => e.Reviews)
            .WithOne(e => e.Product)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
