using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("grocerylist")]
public class GroceryListEntity
{
    [Key] 
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("household_id")]
    [ForeignKey("household")]
    public int HouseholdId { get; set; }
    
}