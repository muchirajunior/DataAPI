using DataAPI.Data;
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

    [HttpGet("")]
    public IActionResult GetAllUsers()=> userService.GetAllUsers();
    

    [HttpGet("{id}")]
    
    public IActionResult GetUser(int id)=> userService.GetUser(id);
 
    [HttpPost("")]
    public  IActionResult AddUser([FromBody]RegisterUser registerUser)=>userService.RegisterUser(registerUser);

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody]LoginUser user)=>userService.LoginUser(user);

    [HttpDelete("{id}")]
    [Authorize(Roles="Admin,Manager")]
    public IActionResult DeleteUser(int id)=>userService.DeleteUser(id);
    
}
