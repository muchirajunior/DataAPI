using System.ComponentModel.DataAnnotations;

namespace DataAPI.Data;

public class LoginUser{
    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Password { get;  set; }    
}