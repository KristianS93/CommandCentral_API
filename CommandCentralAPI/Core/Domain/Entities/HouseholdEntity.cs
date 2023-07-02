using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("household")]
public class HouseholdEntity
{
    [Key] 
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("grocerlist_id")]
    public GroceryListEntity? grocery_list { get; set; }
}