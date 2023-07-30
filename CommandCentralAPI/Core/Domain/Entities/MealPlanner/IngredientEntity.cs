using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.MealPlanner;
[Table("ingredient")]
public class IngredientEntity : BaseEntity
{
    [Column("ingredient_name")]
    public string Name { get; set; }
    
    [Column("ingredient_amount")]
    public string Amount { get; set; }

    [Column("meal_id")]
    [Required]
    [JsonIgnore]
    public int MealId { get; set; }
    
    [JsonIgnore]
    public MealEntity Meal { get; set; }
}