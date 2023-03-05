using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public string? Password { get; set; }
    [MaxLength(100)]
    [EmailAddress]
    public string? Email { get; set; }
    public bool IsVerified { get; set; }=false;
    [Required]
    public string? Role { get; set; }
    public Business? UserBusiness { get; set; }
}