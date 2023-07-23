using Domain.Entities;
using Domain.Models.Authentication;

namespace Infrastructure.Authentication.Interfaces;

public interface IJwtProvider
{
    string Generate(HouseholdEntity household, GroceryListEntity? groceryListId, Permission permission);
}