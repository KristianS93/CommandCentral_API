using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("grocery_list")]
public class GroceryListEntity
{
    [Key] 
    [Column("id")]
    public int Id { get; set; }
    [ForeignKey("household_id")]
    public HouseholdEntity? household_ { get; set; }
    public ICollection<GroceryListItemEntity>? items { get; set; }
}