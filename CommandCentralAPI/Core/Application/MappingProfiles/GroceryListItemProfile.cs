using Application.Features.GroceryListItem.Commands.CreateGroceryListItem;
using Application.Features.GroceryListItem.Commands.UpdateGroceryListItem;
using Application.Features.GroceryListItem.Queries.GetGroceryListItem;
using Application.Features.GroceryListItem.Shared;
using AutoMapper;
using Domain.Entities.GroceryList;

namespace Application.MappingProfiles;

public class GroceryListItemProfile : Profile
{
    public GroceryListItemProfile()
    {
        CreateMap<GroceryListItemEntity, GroceryListItemDto>().ReverseMap();
        CreateMap<UpdateGroceryListItemCommand, GroceryListItemEntity>();
        CreateMap<CreateGroceryListItemCommand, GroceryListItemEntity>();
        CreateMap<GroceryListItemEntity, GroceryListItemDetailsDto>();
    }
}