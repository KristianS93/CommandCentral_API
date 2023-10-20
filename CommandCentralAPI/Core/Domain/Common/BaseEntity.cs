using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

/// <summary>
/// Base entity enabling id, and datetime for creation.
/// </summary>
public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    
    public virtual DateTime LastModified { get; set; }
}