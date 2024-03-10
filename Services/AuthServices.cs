using DataAPI.Data;
using DataAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Services;

public class AuthService : IAuthService {
    private readonly UserManager<User> userManager;

    public AuthService(UserManager<User> userManager){
        this.userManager = userManager;
    }

    public async Task<IActionResult> RegisterUser(RegisterUser user){
        var newUser = new User(){
            Email = user.Email,
            FullName = user.Name,
            // BusinessId =  user.BusinessId,
            // Role = user.Role
        };
        var result = await userManager.CreateAsync(newUser,user.Password);
        return new OkObjectResult(new {message="success "});
    }
}