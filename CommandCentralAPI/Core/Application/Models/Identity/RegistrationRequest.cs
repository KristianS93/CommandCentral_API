using System.ComponentModel.DataAnnotations;

namespace Application.Models.Identity;

public class RegistrationRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MinLength(3)]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    [MinLength(4)]
    public string Password { get; set; } = string.Empty;
}