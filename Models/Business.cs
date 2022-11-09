using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models;

public class Business{
    [Key]
    public int ID { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessOwner { get; set; }
    public bool IsActive { get; set; }=false;
    public List<Product>? Products { get; set; }

    public int UserID { get; set; }
    public User? User { get; set; }
}