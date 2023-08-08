using Application.Contracts.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationMember> _userManager;

    public UserService(UserManager<ApplicationMember> userManager)
    {
        _userManager = userManager;
    }
    public async Task<List<Member>> GetMembers()
    {
        var members = await _userManager.GetUsersInRoleAsync("Member");
        return members.Select(q => new Member
        {
            Id = q.Id,
            Email = q.Email,
            FirstName = q.FirstName,
            LastName = q.LastName
        }).ToList();
    }

    public async Task<Member> GetMember(string userId)
    {
        var member = await _userManager.FindByIdAsync(userId);
        return new Member
        {
            Email = member.Email,
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName
        };
    }

    public async Task CreateHousehold(int householdId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(user);
        user.HouseholdId = householdId;
        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteHousehold(int householdId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(user);
        user.HouseholdId = null;
        await _userManager.UpdateAsync(user);
    }

    public async Task CreateGroceryList(int groceryListId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(user);
        user.GroceryListId = groceryListId;
        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteGroceryList(int groceryListId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(user);
        user.GroceryListId = null;
        await _userManager.UpdateAsync(user);
    }
}