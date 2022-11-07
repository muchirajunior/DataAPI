using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI ;
public class DatabaseContext : DbContext {
    public DatabaseContext() { }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }

    
    public DbSet<Item>? Items { get; set; }
}
