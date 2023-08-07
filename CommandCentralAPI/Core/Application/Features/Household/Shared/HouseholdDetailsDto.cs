namespace Application.Features.Household.Shared;

public class HouseholdDetailsDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = String.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime LastModified { get; set; }
}