using DataAPI.Models;
using DataAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using DataAPI.Models;

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
    public  IActionResult AddUser([FromBody]User user)=>userService.RegisterUser(user);

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody]LoginUser user)=>userService.LoginUser(user);

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteUser(int id)=>userService.DeleteUser(id);
    
}
