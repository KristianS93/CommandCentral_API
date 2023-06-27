using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandCentralAPI.dbmodels;

[Table("grocery_list_item")]
public class DbGroceryListItem
{
    [Key]
    public int id { get; set; }
    [Required]
    public DbGroceryList grocery_list_ { get; set; }
    [Required] public string item_name { get; set; }
    [Required] public string item_amount { get; set; }
}