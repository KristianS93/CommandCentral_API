using Application.Features.GroceryList.Commands.CreateGroceryList;
using Domain.Entities.GroceryList;

namespace Application.Features.GroceryList;

public static class GroceryListMapping
{
    public static GroceryListEntity ToEntity(this CreateGroceryListCommand command)
    {
        return new GroceryListEntity
        {
            HouseholdId = command.HouseholdId
        };
    }

    public static CreateGroceryListDto ToDTO(this GroceryListEntity entity)
    {
        return new CreateGroceryListDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt
        };
    }
}