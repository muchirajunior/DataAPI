using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models;

public class User{
    [Key]
    public int ID { get; set; }
    [MaxLength(200)]
    public string? FullName { get; set; }
    [MaxLength(100)]
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [MaxLength(100)]
    [EmailAddress]
    public string? Email { get; set; }
    public bool IsVerified { get; set; }=false;
    public Business? UserBusiness { get; set; }
}