using DataAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAPI ;
public class DatabaseContext : DbContext {
    private readonly IConfiguration configuration;

    public DatabaseContext(IConfiguration configuration){
        this.configuration = configuration;
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base (options){
        this.configuration = configuration;
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<IdentityUser> IdentityUsers { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; } //junction table

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        if (!optionsBuilder.IsConfigured){
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DatabaseConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        
        modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique(); 
        
        modelBuilder.Entity<Order>() .HasMany(order=> order.Products).WithMany(product=>product.Orders).UsingEntity<OrderProduct>();
    }
}