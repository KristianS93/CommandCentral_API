using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.MealPlanner;

[Table("meal")]
public class MealEntity : BaseEntity
{
    [Column("meal_name")]
    public string Name { get; set; }
    [Column("meal_description")]
    public string Description { get; set; }
    [Column("meal_direction")]
    public string Directions { get; set; }
    
    [Column("household_id")]
    [Required]
    [JsonIgnore]
    public int HouseholdId { get; set; }
    
    [JsonIgnore]
    public HouseholdEntity? Household{ get; set; }
}