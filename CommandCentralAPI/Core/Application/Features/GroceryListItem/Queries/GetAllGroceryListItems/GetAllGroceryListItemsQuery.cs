using Application.Features.GroceryListItem.Shared;
using MediatR;

namespace Application.Features.GroceryListItem.Queries.GetAllGroceryListItems;

public record GetAllGroceryListItemsQuery(int GroceryListId) : IRequest<List<GetGroceryListItemDto>>;