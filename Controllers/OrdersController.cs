using DataAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly DatabaseContext context;

    public OrdersController(DatabaseContext context){
        this.context = context;
    }

    [HttpGet("")]
    public IActionResult GetAllOrders()=> Ok(context.Orders!.ToList());

    [HttpGet("{id}")]
    public IActionResult GetOrderById([FromRoute] int id)=>Ok(context.Orders!.Where(order=>order.ID==id).Include(order=>order.Products).ToList().FirstOrDefault());

    [HttpPost("")]
    public IActionResult CreateOrder([FromBody]Order order){
       context.Orders!.Add(order);
       context.SaveChanges();
       return Created("",order);
    }

    [HttpPut("{id}")]
    public IActionResult AddOrderProducts([FromRoute]int id,[FromBody]int [] ProductIds) {
        foreach (var produtId in ProductIds){
            context.OrderProducts!.Add(new OrderProduct(){ProductID=produtId,OrderID=id});
        }
        context.SaveChanges();
        var order = context.Orders!.Where(order=>order.ID==id).Include(order=>order.Products).ToList().FirstOrDefault();
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var order= context.Orders!.Where(order=>order.ID==id).FirstOrDefault();
        if (order == null){
            return BadRequest(new {message="failed to delete order",error="invalid order id"});
        }
        context.Orders!.Remove(order);
        context.SaveChanges();

        return Ok(new {message="order deleted successfully"});
    }
   
}
