using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data;

public class SkinCareManagementDbContext : DbContext
{
    public SkinCareManagementDbContext(DbContextOptions<SkinCareManagementDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Volume> Volumes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Volume)
            .WithMany(v => v.Products)
            .HasForeignKey(p => p.VolumeId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<ProductImage>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.ProductId);

        base.OnModelCreating(modelBuilder);
    }
} 