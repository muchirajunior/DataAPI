using DataAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Services;

public class AuthService : IAuthService {
    private readonly UserManager<IdentityUser> userManager;

    public AuthService(UserManager<IdentityUser> userManager){
        this.userManager = userManager;
    }

    public async Task<IActionResult> RegisterUser(RegisterUser user){
        var newUser = new IdentityUser(){
            Email = user.Email,
            UserName = user.Username,
        };
        var result = await userManager.CreateAsync(newUser,user.Password);
        return new OkObjectResult(new {result});
    }
}