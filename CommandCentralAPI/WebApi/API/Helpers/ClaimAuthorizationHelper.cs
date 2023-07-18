namespace API.Helpers;

public static class ClaimAuthorizationHelper
{
    public static void ConfirmHouseholdClaim(string claim, int householdId)
    {
        if (Convert.ToInt32(claim) != householdId)
        {
            // Ændre til korrekt exception
            throw new ArgumentException("Incorrect household id");
        }
    }
}