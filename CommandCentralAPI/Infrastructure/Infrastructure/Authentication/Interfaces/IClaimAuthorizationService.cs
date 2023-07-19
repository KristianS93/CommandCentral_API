namespace Infrastructure.Authentication.Interfaces;

public interface IClaimAuthorizationService
{ 
    void ConfirmHouseholdClaim(string claim, int householdId);
}