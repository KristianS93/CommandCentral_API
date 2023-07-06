using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;
[Table("grocerylist_item")]
public class GroceryListItemEntity
{
    [Key]
    [Column("id")]
    public int GroceryListItemId { get; set; }

    [Required]
    [Column("item_name")]
    public string ItemName { get; set; }
    
    [Required]
    [Column("item_amount")]
    public int ItemAmount { get; set; }
    
    [Required]
    [Column("grocerylist_id")]
    [JsonIgnore]
    public int GroceryListId { get; set; }
    
    [JsonIgnore]
    public GroceryListEntity? GroceryList { get; set; }
}