using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.MealPlanner;

[Table("tag")]
public class TagEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("tag_name")]
    public string TagName { get; set; }
    
    [Column("meal_id")]
    public int MealId { get; set; }
    
    [Column("household_id")]
    public int HouseholdId { get; set; }

    public MealEntity Meal { get; set; }

    public HouseholdEntity Household { get; set; }
}