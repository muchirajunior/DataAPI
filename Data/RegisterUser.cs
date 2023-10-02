using System.ComponentModel.DataAnnotations;

namespace DataAPI.Data;

public class RegisterUser{
    public string? Name { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    public string? Email { get; set; }
    [Required]
    public string? Role { get; set; }
}