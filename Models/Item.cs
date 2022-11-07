using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models;

public class Item{
    [Key]
    public int ID { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
}