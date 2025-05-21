using API.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Repositories
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.NewPrice).HasPrecision(18, 2);
            builder.Property(p => p.OldPrice).HasPrecision(18, 2);
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Photos)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
                new Product { Id = 1, Name = "Smartphone", Description = "Latest model smartphone", NewPrice = 699.99m, OldPrice = 9.99m , CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", Description = "High-performance laptop", NewPrice = 1299.99m, OldPrice = 9.99m , CategoryId = 1 },
                new Product { Id = 3, Name = "T-shirt", Description = "Comfortable cotton t-shirt", NewPrice = 19.99m , OldPrice = 9.99m, CategoryId = 2 },
                new Product { Id = 4, Name = "Washing Machine", Description = "Energy-efficient washing machine", NewPrice = 499.99m, OldPrice = 9.99m , CategoryId = 3 }
            );
        }
    }
}
