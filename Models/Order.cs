using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models;

public class Order{
    [Key]
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public double Total { get; set; }
    public List<Product>? Products { get; set; }
}
