using Application.Contracts.GroceryList;
using Application.Features.GroceryListItem.Shared;
using MediatR;

namespace Application.Features.GroceryListItem.Queries.GetAllGroceryListItems;

public class GetAllGroceryListItemsQueryHandler : IRequestHandler<GetAllGroceryListItemsQuery, List<GroceryListItemDto>>
{
    private readonly IGroceryListItemRepository _groceryListRepository;

    public GetAllGroceryListItemsQueryHandler(IGroceryListItemRepository groceryListRepository)
    {
        _groceryListRepository = groceryListRepository;
    }

    public async Task<List<GroceryListItemDto>> Handle(GetAllGroceryListItemsQuery request, CancellationToken cancellationToken)
    {
        var groceryListItems = await _groceryListRepository.GetGroceryListItemAsync(request.GroceryListId);
        
        ArgumentNullException.ThrowIfNull(groceryListItems);

        var listData = groceryListItems.ToListDTO();
        
        return listData;
    }
}