using DataAPI.Models;
using Microsoft.AspNetCore.Mvc;
//using DataAPI.Models;

namespace DataAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : Controller
{
    private readonly DatabaseContext databaseContext;

    public ProductsController(DatabaseContext databaseContext){
        this.databaseContext = databaseContext;
    }

    [HttpGet("")]
    public IActionResult GetAllProducts()=>Ok(databaseContext.Products);

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)=>Ok(databaseContext.Products!.Where(product=>product.ID==id).FirstOrDefault());

    [HttpPost("")]
    public IActionResult AddNewProduct([FromBody]Product product)
    {
        try{
            databaseContext.Products!.Add(product);
            databaseContext.SaveChanges();
            return Created("products",new {message="added product successfully",product}); 
        }catch (System.Exception error){
            return BadRequest(new {message="failed to add new product",error});
        }
        
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        Product? bs=databaseContext.Products!.Where(prod=>prod.ID==id).FirstOrDefault();
        if (bs==null){
            return BadRequest(new {message="failed to delete, product does not exist"});
        }
        databaseContext.Remove(bs);
        databaseContext.SaveChanges();
        return Ok(new {message="deleted product successfully"});
    }
    
       
}
