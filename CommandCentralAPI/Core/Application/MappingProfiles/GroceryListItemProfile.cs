using Application.Features.GroceryListItem.Shared;
using AutoMapper;
using Domain.Entities.GroceryList;

namespace Application.MappingProfiles;

public class GroceryListItemProfile : Profile
{
    public GroceryListItemProfile()
    {
        CreateMap<GroceryListItemEntity, GetGroceryListItemDto>().ReverseMap();
    }
}