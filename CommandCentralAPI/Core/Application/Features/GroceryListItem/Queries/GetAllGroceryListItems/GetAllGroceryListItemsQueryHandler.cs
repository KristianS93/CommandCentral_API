using Application.Contracts.GroceryList;
using Application.Contracts.Hub;
using Application.Features.GroceryListItem.Shared;
using AutoMapper;
using MediatR;

namespace Application.Features.GroceryListItem.Queries.GetAllGroceryListItems;

public class GetAllGroceryListItemsQueryHandler : IRequestHandler<GetAllGroceryListItemsQuery, List<GroceryListItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IGroceryListItemRepository _groceryListRepository;

    public GetAllGroceryListItemsQueryHandler(IMapper mapper, IGroceryListItemRepository groceryListRepository)
    {
        _mapper = mapper;
        _groceryListRepository = groceryListRepository;
    }

    public async Task<List<GroceryListItemDto>> Handle(GetAllGroceryListItemsQuery request, CancellationToken cancellationToken)
    {
        var groceryListItems = await _groceryListRepository.GetGroceryListItemAsync(request.GroceryListId);
        
        ArgumentNullException.ThrowIfNull(groceryListItems);

        var listData = _mapper.Map<List<GroceryListItemDto>>(groceryListItems);
        
        return listData;
    }
}