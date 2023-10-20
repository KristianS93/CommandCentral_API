using Application.Contracts.GroceryList;
using Application.Exceptions;
using MediatR;

namespace Application.Features.GroceryList.Commands.DeleteGroceryList;

public class DeleteGroceryListCommandHandler : IRequestHandler<DeleteGroceryListCommand, Unit>
{
    private readonly IGroceryListRepository _groceryListRepository;

    public DeleteGroceryListCommandHandler(IGroceryListRepository groceryListRepository)
    {
        _groceryListRepository = groceryListRepository;
    }
    public async Task<Unit> Handle(DeleteGroceryListCommand request, CancellationToken cancellationToken)
    {
        if (await _groceryListRepository.IsOwnerOfGroceryList(request.ListId, request.HouseholdId) == false)
        {
            throw new AuthorizationException("Access Denied");
        }

        var list = await _groceryListRepository.GetByIdAsync(request.ListId);
        ArgumentNullException.ThrowIfNull(list);

        await _groceryListRepository.DeleteAsync(list);
        
        return Unit.Value;
    }
}