using MediatR;

namespace Application.Features.GroceryListItem.Queries.GetGroceryListItem;

public record GetGroceryListItemQuery(int ItemId, int GroceryListId) : IRequest<GroceryListItemDetailsDto>;
