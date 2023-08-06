using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;
using Domain.Entities.Household;

namespace Domain.Entities.GroceryList;

public class GroceryListEntity : BaseEntity
{
    public int HouseholdId { get; set; }
    
    public HouseholdEntity? Household { get; set; }
}