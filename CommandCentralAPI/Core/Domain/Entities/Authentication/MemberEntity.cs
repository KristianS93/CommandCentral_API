using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Authentication;

namespace Domain.Entities.Authentication;

[Table("members")]
public class MemberEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("username")]
    public string Username { get; set; }
    
    [Required]
    [Column("password")]
    public string Password { get; set; }
    
    [Column("authority")]
    public Permission Permission { get; set; }
    
    [Column("household_id")]
    public int HouseholdId { get; set; }
}