using Application.Contracts.GroceryList;
using Application.Exceptions;
using Application.Features.GroceryListItem.Shared;
using MediatR;

namespace Application.Features.GroceryListItem.Commands.CreateGroceryListItem;

public class CreateGroceryListItemCommandHandler :  IRequestHandler<CreateGroceryListItemCommand, GroceryListItemDto>
{
    private readonly IGroceryListItemRepository _groceryListItemRepository;
    public CreateGroceryListItemCommandHandler(IGroceryListItemRepository groceryListItemRepository)
    {
        _groceryListItemRepository = groceryListItemRepository;
    }
    public async Task<GroceryListItemDto> Handle(CreateGroceryListItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateGroceryListItemValidator();
        var validationResults = await validator.ValidateAsync(request);

        if (validationResults.Errors.Any())
        {
            throw new BadRequestException("Invalid grocery list item", validationResults);
        }

        var entity = request.ToEntity();

            var dto = (await _groceryListItemRepository.CreateAsync(entity)).ToDTO();
        
        return dto;
    }
}