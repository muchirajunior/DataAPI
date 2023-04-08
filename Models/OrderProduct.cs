//juction table
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAPI.Models;

public class OrderProduct{
    [Key]
    public int ID { get; set; }
    public int ProductID { get; set; }

    [ForeignKey("ProductID")]
    public Product? Product { get; set; }
    public int OrderID { get; set; }

    [ForeignKey("OrderID")]
    public Order? Order { get; set; }

}