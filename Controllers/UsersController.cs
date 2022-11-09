using DataAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using DataAPI.Models;

namespace DataAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public UsersController( UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

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
    public async Task<IActionResult> AddUser([FromBody]IdentityUser user)
    {
        Console.WriteLine(user.UserName);   
        // await userManager.CreateAsync(user);
        return Ok(new {message="user created successfully"});
    }
    
}
