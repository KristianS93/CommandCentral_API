using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

[Table("grocerylist")]
public class GroceryListEntity
{
    [Key] 
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("household_id")]
    public int HouseholdId { get; set; }
    [JsonIgnore]
    public HouseholdEntity Household { get; set; }
}