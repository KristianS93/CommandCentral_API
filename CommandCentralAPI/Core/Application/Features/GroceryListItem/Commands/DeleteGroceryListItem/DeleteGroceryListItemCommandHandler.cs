using Application.Contracts.GroceryList;
using Application.Exceptions;
using MediatR;

namespace Application.Features.GroceryListItem.Commands.DeleteGroceryListItem;

public class DeleteGroceryListItemCommandHandler : IRequestHandler<DeleteGroceryListItemCommand, Unit>
{
    private readonly IGroceryListItemRepository _groceryListItemRepository;

    public DeleteGroceryListItemCommandHandler(IGroceryListItemRepository groceryListItemRepository)
    {
        _groceryListItemRepository = groceryListItemRepository;
    }
    public async Task<Unit> Handle(DeleteGroceryListItemCommand request, CancellationToken cancellationToken)
    {
        // Get item 
        var item = await _groceryListItemRepository.GetByIdAsync(request.GroceryListItemId);
        
        ArgumentNullException.ThrowIfNull(item);
        
        // check item is owned by the grocery list 
        if (await _groceryListItemRepository.IsOwnerOfItem(request.GroceryListId, request.GroceryListItemId) == false)
        {
            throw new AuthorizationException("Access denied");
        }
        
        // delete
        await _groceryListItemRepository.DeleteAsync(item);
        
        return Unit.Value;
    }
}