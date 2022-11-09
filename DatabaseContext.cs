using DataAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAPI ;
public class DatabaseContext : DbContext {
    public DatabaseContext() { }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }
   
    public DbSet<Product>? Products { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Business>? Businesses { get; set; }
}
