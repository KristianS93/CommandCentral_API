using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandCentralAPI.Models;

[Table("products")]
public class Products
{
    [Key, Required] public int id { get; set; }
    [Required] public string? name { get; set; }
    public string? brand { get; set; }
    public string? size { get; set; }
    public decimal price { get; set; }
}