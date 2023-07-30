using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.MealPlanner;

[Table("week_plan")]
public class WeekPlanEntity : BaseEntity
{
    
    [Column("day_1")]
    public int Day1 { get; set; }
    
    [Column("day_2")]
    public int Day2 { get; set; }
    
    [Column("day_3")]
    public int Day3 { get; set; }
    
    [Column("day_4")]
    public int Day4 { get; set; }
    
    [Column("day_5")]
    public int Day5 { get; set; }
    
    [Column("day_6")]
    public int Day6 { get; set; }
    
    [Column("day_7")]
    public int Day7 { get; set; }
    
    [Required]
    [JsonIgnore]
    [Column("household_id")]
    public int HouseholdId { get; set; }
    
    [JsonIgnore]
    public HouseholdEntity? Household { get; set; }
    
}