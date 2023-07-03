using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("grocerylist")]
public class GroceryListEntity
{
    [Key] 
    [Column("grocerylist_id")]
    public int GroceryListID { get; set; }
    
    [Required]
    [Column("household_id")]
    public int HouseholdID { get; set; }
    
    [Required, Column("creation_date")]
    public DateTime CreationDate { get; set; }
    
    [ForeignKey("HouseholdID")]
    public HouseholdEntity household{ get; set; }

    public ICollection<GroceryListItemEntity> grocerylist_items { get; set; }
}