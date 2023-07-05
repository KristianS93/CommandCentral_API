using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("household")]
public class HouseholdEntity
{
    [Key] 
    [Column("id")]
    public int Id { get; set; }
    
    [Required, Column("name")]
    public string Name { get; set; }
}