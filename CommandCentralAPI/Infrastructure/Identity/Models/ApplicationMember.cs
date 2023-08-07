using Microsoft.AspNetCore.Identity;

namespace Identity.Models;

public class ApplicationMember : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int? HouseholdId { get; set; }
}