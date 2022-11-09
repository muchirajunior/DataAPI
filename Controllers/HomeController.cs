using Microsoft.AspNetCore.Mvc;
//using DataAPI.Models;

namespace DataAPI.Controllers;
[ApiController]
public class HomeController : Controller
{
    public HomeController(){}

    [HttpGet("")]
    public IActionResult Index()
    {
        return Ok(new {message="Data API V1", status="OK"});
    }
}
