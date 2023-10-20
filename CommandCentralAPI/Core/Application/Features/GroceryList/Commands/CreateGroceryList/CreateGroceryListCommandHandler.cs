using Application.Contracts.GroceryList;
using MediatR;

namespace Application.Features.GroceryList.Commands.CreateGroceryList;

public class CreateGroceryListCommandHandler : IRequestHandler<CreateGroceryListCommand, CreateGroceryListDto>
{
    private readonly IGroceryListRepository _groceryListRepository;

    public CreateGroceryListCommandHandler(IGroceryListRepository groceryListRepository)
    {
        _groceryListRepository = groceryListRepository;
    }
    public async Task<CreateGroceryListDto> Handle(CreateGroceryListCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();
        var result = await _groceryListRepository.CreateAsync(entity);
        
        return result.ToDTO();
    }
}