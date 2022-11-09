using DataAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using DataAPI.Models;

namespace DataAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    public UsersController(){}

    [HttpGet("")]
    public IActionResult GetAllUsers()
    {
        // TODO: Your code here
        return Ok(new {message="all users in the system",data=new List<int>(){},});
    }
    

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        return Ok();
    }
    [HttpPost("")]
    public  IActionResult AddUser()
    {  
        return Ok(new {message="user created successfully"});
    }
    
}
