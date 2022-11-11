using System.ComponentModel.DataAnnotations;

namespace DataAPI.Services;

public class LoginUser{
    [Required]
    [MaxLength(100)]
    public string? Username { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Password { get;  set; }    
}