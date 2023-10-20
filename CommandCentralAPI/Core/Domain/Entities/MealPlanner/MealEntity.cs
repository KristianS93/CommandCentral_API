using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;
using Domain.Entities.Household;

namespace Domain.Entities.MealPlanner;

public class MealEntity : BaseEntity
{
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; } 
    public string? Directions { get; set; }
    public string? Tags { get; set; } 
    public int HouseholdId { get; set; }
    
    public HouseholdEntity? Household{ get; set; }
}