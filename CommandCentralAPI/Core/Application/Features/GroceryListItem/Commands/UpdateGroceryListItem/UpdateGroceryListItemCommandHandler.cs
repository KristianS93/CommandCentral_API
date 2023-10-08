using Application.Contracts.GroceryList;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities.GroceryList;
using MediatR;

namespace Application.Features.GroceryListItem.Commands.UpdateGroceryListItem;

public class UpdateGroceryListItemCommandHandler : IRequestHandler<UpdateGroceryListItemCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IGroceryListItemRepository _groceryListItemRepository;

    public UpdateGroceryListItemCommandHandler(IMapper mapper,IGroceryListItemRepository groceryListItemRepository)
    {
        _mapper = mapper;
        _groceryListItemRepository = groceryListItemRepository;
    }
    public async Task<Unit> Handle(UpdateGroceryListItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateGroceryListItemValidator();
        var validationResults = await validator.ValidateAsync(request);

        if (validationResults.Errors.Any())
        {
            throw new BadRequestException("Invalid grocery list item", validationResults);
        }
        
        // get grocerylistItem data 
        var groceryListItem = await _groceryListItemRepository.GetByIdAsync(request.GroceryListItemId);
        if (groceryListItem == null)
        {
            throw new NotFoundException(nameof(GroceryListItemEntity), request.GroceryListItemId);
        }
        
        // check the owner of the item is correct 
        if (await _groceryListItemRepository.IsOwnerOfItem(request.GroceryListId, request.GroceryListItemId) == false)
        {
            throw new AuthorizationException("Access denied to item");
        }

        _mapper.Map(request, groceryListItem);

        await _groceryListItemRepository.UpdateAsync(groceryListItem);
        
        return Unit.Value;
    }
}