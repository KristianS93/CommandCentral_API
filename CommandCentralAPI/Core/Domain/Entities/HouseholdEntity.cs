using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("household")]
public class HouseholdEntity
{
    [Key] 
    [Column("household_id")]
    public int HouseholdID { get; set; }
    
    [Required, Column("name")]
    public string Name { get; set; }
}