using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("household")]
public class HouseholdEntity
{
    [Key] public int id { get; set; }
}