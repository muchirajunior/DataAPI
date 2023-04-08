using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAPI.Models;

public class Product{
    [Key]
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }=0;
    public int BusinessID { get; set; }
    [ForeignKey("BusinessID")]
    public Business? Business { get; set; }
    public List<Order>? Orders { get; set; }
}