using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;
using Domain.Entities.Household;

namespace Domain.Entities.MealPlanner;

public class WeekPlanEntity : BaseEntity
{
    public int Day1 { get; set; }
    
    public int Day2 { get; set; }
    
    public int Day3 { get; set; }
    
    public int Day4 { get; set; }
    
    public int Day5 { get; set; }
    
    public int Day6 { get; set; }
    
    public int Day7 { get; set; }
    
    public int HouseholdId { get; set; }
    
    public HouseholdEntity? Household { get; set; }
}