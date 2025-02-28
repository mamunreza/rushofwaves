using Microsoft.EntityFrameworkCore;

namespace MessagePassing.Products.Data;

public interface IAppDbContext
{
    
}

public class AppDbContext(DbContextOptions options) : DbContext(options), IAppDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAddedOutbox> ProductAddedOutboxes { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddedInbox> CustomerAddedInboxes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductAddedOutbox>(entity =>
        {
            entity.Property(e => e.IsProcessed)
                .HasDefaultValue(false);
        });

        modelBuilder.Entity<CustomerAddedInbox>(entity =>
        {
            entity.Property(e => e.IsProcessed)
                .HasDefaultValue(false);
        });
    }
}