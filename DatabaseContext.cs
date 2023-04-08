using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI ;
public class DatabaseContext : DbContext {
    public DatabaseContext() { }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }
   
    public DbSet<Product>? Products { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Business>? Businesses { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderProduct>? OrderProducts { get; set; } //junction table

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique(); 
        
        modelBuilder.Entity<Order>() .HasMany(order=> order.Products).WithMany(product=>product.Orders).UsingEntity<OrderProduct>();
    }
}