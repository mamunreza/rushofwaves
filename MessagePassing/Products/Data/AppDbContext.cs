using Microsoft.EntityFrameworkCore;

namespace MessagePassing.Products.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAddedOutbox> ProductAddedOutboxes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductAddedOutbox>(entity =>
        {
            entity.Property(e => e.IsProcessed)
                .HasDefaultValue(false);
        });
    }
}