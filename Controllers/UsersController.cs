using DataAPI.Models;
using DataAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using DataAPI.Models;

namespace DataAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;
    private readonly DatabaseContext databaseContext;

    public UsersController(IUserService userService, DatabaseContext databaseContext)
    {
        this.userService = userService;
        this.databaseContext = databaseContext;
    }

    [HttpGet("")]
    public IActionResult GetAllUsers()=> Ok(new {message="all users in the system",data=databaseContext.Users,});
    
    

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)=> Ok(databaseContext.Users!.Where(user=>user.ID==id).FirstOrDefault());
 
    [HttpPost("")]
    public  IActionResult AddUser([FromBody]User user)
    {  
        dynamic? results=userService.RegisterUser(user);
        return Ok(results);
    }

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody]LoginUser user)
    {
        dynamic? result=userService.LoginUser(user);
        if (result.login){
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        User? usr=databaseContext.Users!.Where(user=>user.ID==id).FirstOrDefault();
        if (usr==null){
            return BadRequest(new {message="failed to delete, user does not exist"});
        }
        databaseContext.Remove(usr);
        databaseContext.SaveChanges();
        return Ok(new {message="deleted user successfully"});
    }
    
    
}
