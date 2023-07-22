namespace Infrastructure.Authentication.Interfaces;

public interface IClaimAuthorizationService
{ 
    // void ConfirmHouseholdClaim(string claim, int householdId);
    // void ConfirmGroceryListClaim(string claim, int groceryListId);
    
    int GetIntegerClaimId(string claim);
    void ConfirmItemOnList(int groceryListId, int groceryListItemId);
}