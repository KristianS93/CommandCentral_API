using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities.Household;

[Table("household")]
public class HouseholdEntity : BaseEntity
{
    public string Name { get; set; } = String.Empty;

}