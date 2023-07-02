using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
[Table("grocery_list_item")]
public class GroceryListItemEntity
{
    [Key]
    public int id { get; set; }
    [ForeignKey("grocery_list_id")]
    public GroceryListEntity? grocery_list_ { get; set; }
    public string item_name { get; set; }
    public string item_amount { get; set; }
}