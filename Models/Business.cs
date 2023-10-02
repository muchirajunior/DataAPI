using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAPI.Models;

public class Business{
    [Key]
    public int Id { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessOwner { get; set; }
    public bool IsActive { get; set; }=false;
    public List<Product>? Products { get; set; }
    public List<User>? Users { get; set; }
    public List<Customer>? Customers { get; set; }
}