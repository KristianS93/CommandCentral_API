using Application.Models.Identity;

namespace Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Member>> GetMembers();

    Task<Member> GetMember(string userId);

    Task CreateHousehold(int householdId, string userId);
    
    Task DeleteHousehold(int householdId, string userId);

    Task CreateGroceryList(int groceryListId, string userId);
    
    Task DeleteGroceryList(int groceryListId, string userId);
}