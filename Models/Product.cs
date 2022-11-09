using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models;

public class Product{
    [Key]
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }=0;
    public int BusinessID { get; set; }
    public Business? Business { get; set; }
}