namespace Application.Features.Household.Queries.GetHousehold;

public class HouseholdDto
{
    // only the name is relevant as the ID, of a household is in the token.
    public string Name { get; set; } = String.Empty;
}