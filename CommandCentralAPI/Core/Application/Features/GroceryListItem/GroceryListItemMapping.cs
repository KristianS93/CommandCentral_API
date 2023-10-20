using Application.Features.GroceryListItem.Commands.CreateGroceryListItem;
using Application.Features.GroceryListItem.Commands.UpdateGroceryListItem;
using Application.Features.GroceryListItem.Queries.GetGroceryListItem;
using Application.Features.GroceryListItem.Shared;
using Domain.Entities.GroceryList;

namespace Application.Features.GroceryListItem;

public static class GroceryListItemMapping
{
    public static List<GroceryListItemDto> ToListDTO(this List<GroceryListItemEntity> ents)
    {
        var result = new List<GroceryListItemDto>();
        for (int i = 0; i < ents.Count; i++)
        {
            result.Add(
                new GroceryListItemDto
                {
                    Id = ents[i].Id,
                    ItemName = ents[i].ItemName,
                    ItemAmount = ents[i].ItemAmount
                });
        }

        return result;
    }

    public static GroceryListItemDto ToDTO(this GroceryListItemEntity entity)
    {
        return new GroceryListItemDto
        {
            Id = entity.Id,
            ItemName = entity.ItemName,
            ItemAmount = entity.ItemAmount
        };
    }

    public static GroceryListItemEntity ToEntity(this UpdateGroceryListItemCommand command, GroceryListItemEntity entity)
    {
        return new GroceryListItemEntity
        {
            Id = command.GroceryListItemId,
            ItemName = command.ItemName,
            ItemAmount = command.ItemAmount,
            GroceryListId = entity.GroceryListId,
            CreatedAt = entity.CreatedAt,
            LastModified = entity.LastModified
        };
    }
    public static GroceryListItemEntity ToEntity(this CreateGroceryListItemCommand command)
    {
        return new GroceryListItemEntity
        {
            GroceryListId = command.GroceryListId,
            ItemName = command.ItemName,
            ItemAmount = command.ItemAmount
        };
    }
    
    public static GroceryListItemDetailsDto ToDetailsDTO(this GroceryListItemEntity entity)
    {
        return new GroceryListItemDetailsDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            ItemAmount = entity.ItemAmount,
            ItemName = entity.ItemName,
            LastModified = entity.LastModified
        };
    }
    
    
}