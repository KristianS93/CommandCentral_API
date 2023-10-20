using Application.Contracts.GroceryList;
using Application.Exceptions;
using MediatR;

namespace Application.Features.GroceryListItem.Queries.GetGroceryListItem;

public class GetGroceryListItemQueryHandler : IRequestHandler<GetGroceryListItemQuery, GroceryListItemDetailsDto>
{
    private readonly IGroceryListItemRepository _groceryListItemRepository;

    public GetGroceryListItemQueryHandler(IGroceryListItemRepository groceryListItemRepository)
    {
        _groceryListItemRepository = groceryListItemRepository;
    }
    public async Task<GroceryListItemDetailsDto> Handle(GetGroceryListItemQuery request, CancellationToken cancellationToken)
    {
        // check id
        if (await _groceryListItemRepository.IsOwnerOfItem(request.GroceryListId, request.ItemId) == false)
        {
            throw new AuthorizationException("Access denied to item");
        }

        var item = await _groceryListItemRepository.GetByIdAsync(request.ItemId);

        ArgumentNullException.ThrowIfNull(item);
        
        return item.ToDetailsDTO();
    }
}