using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("created_at")] 
    public DateTime CreatedAt { get; set; }

    [Column("last_modified")]
    public DateTime LastModified { get; set; }
}