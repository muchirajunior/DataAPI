using DataAPI.Data;
using DataAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService){
        this.authService = authService;
    }

    [HttpPost("")]
    public async Task<IActionResult> RegisterUser([FromBody]RegisterUser user)=> await authService.RegisterUser(user);

}