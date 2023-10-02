
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAPI.Models;

public class Customer{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Phone]
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public bool AllowCredit { get; set; }=false;
    public DateTime CreatedAt { get; set; }=DateTime.Now;
    public int BusinessId { get; set; }
    [ForeignKey("BusinessId")]
    public Business? Business { get; set; }
}