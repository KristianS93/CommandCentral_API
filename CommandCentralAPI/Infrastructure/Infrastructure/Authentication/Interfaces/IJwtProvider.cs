using Domain.Entities;
using Domain.Entities.GroceryList;
using Domain.Entities.Household;
using Domain.Models.Authentication;

namespace Infrastructure.Authentication.Interfaces;

public interface IJwtProvider
{
    string Generate(HouseholdEntity household, GroceryListEntity? groceryListId, Permission permission);
}