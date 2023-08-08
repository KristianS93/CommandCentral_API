using Application.Contracts.GroceryList;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.GroceryListItem.Queries.GetGroceryListItem;

public class GetGroceryListItemQueryHandler : IRequestHandler<GetGroceryListItemQuery, GroceryListItemDetailsDto>
{
    private readonly IGroceryListItemRepository _groceryListItemRepository;
    private readonly IMapper _mapper;

    public GetGroceryListItemQueryHandler(IGroceryListItemRepository groceryListItemRepository, IMapper mapper)
    {
        _groceryListItemRepository = groceryListItemRepository;
        _mapper = mapper;
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
        
        return _mapper.Map<GroceryListItemDetailsDto>(item);
    }
}