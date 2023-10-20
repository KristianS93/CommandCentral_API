using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Household;

namespace Domain.Entities.MealPlanner;

public class TagEntity : BaseEntity
{
    
    public string TagName { get; set; } = String.Empty;
    
    public int MealId { get; set; }
    
    public int HouseholdId { get; set; }

    public MealEntity? Meal { get; set; }

    public HouseholdEntity? Household { get; set; }
}