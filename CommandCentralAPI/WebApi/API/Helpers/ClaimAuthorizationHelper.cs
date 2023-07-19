using System.Security.Authentication;

namespace API.Helpers;

public static class ClaimAuthorizationHelper
{
    public static void ConfirmHouseholdClaim(string claim, int householdId)
    {
        if (Convert.ToInt32(claim) != householdId)
        {
            throw new AuthenticationException();
        }
    }
}