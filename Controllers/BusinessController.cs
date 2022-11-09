
using DataAPI.Models;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using DataAPI.Models;

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
    


    
    
    
}
