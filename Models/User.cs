using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema; 

namespace DataAPI.Models;

public class User{
    [Key]
    public int Id { get; set; }
    [MaxLength(200)]
    public string? FullName { get; set; }
    [Required]
    [JsonIgnore]
    public string? Password { get; set; }
    [MaxLength(100)]
    [EmailAddress]
    public string? Email { get; set; }
    public bool IsVerified { get; set; }=false;
    [Required]
    public string? Role { get; set; }
    public int BusinessId { get; set; }
    [ForeignKey("BusinessId")]
    public Business? UserBusiness { get; set; }
}