using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities.MealPlanner;
public class IngredientEntity : BaseEntity
{
    public string Name { get; set; } = String.Empty;

    public string Amount { get; set; } = String.Empty;

    public int MealId { get; set; }
    
    public MealEntity? Meal { get; set; }
}