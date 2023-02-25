using DataAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class BusinessController : Controller
{
    private readonly DatabaseContext databaseContext;

    public BusinessController(DatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    [HttpGet("")]
    public IActionResult GetAllBusiness()=>Ok(databaseContext.Businesses!.Include(bs=>bs.Products).ToList());

    [HttpGet("{id}")]
    public IActionResult GetBusinessById(int id)=>Ok(databaseContext.Businesses!.Where(bs=>bs.ID==id).Include(bs=>bs.Products).ToList().FirstOrDefault()  );

    [HttpPost("")]
    public IActionResult AddNewBusiness([FromBody]Business business)
    {
        try {
            databaseContext.Businesses!.Add(business);
            databaseContext.SaveChanges();
            return Created("business",new {message="created item success",business}); 
         }catch (System.Exception error){
            return BadRequest(new {message="failed to add new buiness",error});
        }
        
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteBusiness(int id)
    {
        Business? bs=databaseContext.Businesses!.Where(bs=>bs.ID==id).FirstOrDefault();
        if (bs==null){
            return BadRequest(new {message="failed to delete, business does not exist"});
        }
        databaseContext.Remove(bs);
        databaseContext.SaveChanges();
        return Ok(new {message="deleted business successfully"});
    }

    [HttpGet("{id}/products")]
    public IActionResult GetBusinessProducts( [FromRoute] int id,[FromQuery]int number){ //number can be initialized incase of missing query parameter
        Console.WriteLine("Query parameter number :{0}",number);
        return Ok(databaseContext.Products!.Where(product=>product.BusinessID==id).Take(number).OrderBy(product=>product.ID).ToList());
    }

    
    
    
}
