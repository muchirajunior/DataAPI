using System.ComponentModel.DataAnnotations;

namespace DataAPI.Data;

public class RegisterUser{
    public required string Name { get; set; }
    [Required]
    public required string Password { get; set; }
    public string? Email { get; set; }
    [Required]
    public string? Role { get; set; }
    [Required]
    public int BusinessId { get; set; }
}