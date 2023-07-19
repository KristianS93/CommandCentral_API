namespace Infrastructure.Authentication.Interfaces;

public interface IClaimAuthorizationService
{ 
    void ConfirmHouseholdClaim(string claim, int householdId);
    void ConfirmGroceryListClaim(string claim, int groceryListId);
}