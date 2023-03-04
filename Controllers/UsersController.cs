using DataAPI.Models;
using DataAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DataAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet(""),MapToApiVersion("2.0")]
    public dynamic GetAllUsers()=> userService.GetAllUsers();
    

    [HttpGet("{id}")]
    
    public dynamic GetUser(int id)=> userService.GetUser(id);
 
    [HttpPost("")]
    public  dynamic AddUser([FromBody]User user)=>userService.RegisterUser(user);

    [HttpPost("login")]
    public dynamic LoginUser([FromBody]LoginUser user)=>userService.LoginUser(user);

    [HttpDelete("{id}")]
    [Authorize(Roles="Admin,Manager")]
    public dynamic DeleteUser(int id)=>userService.DeleteUser(id);
    
}
