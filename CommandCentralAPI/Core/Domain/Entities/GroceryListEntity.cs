using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("grocery_list")]
public class GroceryListEntity
{
    [Key] public int id { get; set; }
    public HouseholdEntity household_ { get; set; }
}