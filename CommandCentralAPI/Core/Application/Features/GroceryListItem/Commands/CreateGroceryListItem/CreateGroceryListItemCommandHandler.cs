using Application.Contracts.GroceryList;
using Application.Exceptions;
using Application.Features.GroceryListItem.Shared;
using AutoMapper;
using Domain.Entities.GroceryList;
using FluentValidation;
using MediatR;

namespace Application.Features.GroceryListItem.Commands.CreateGroceryListItem;

public class CreateGroceryListItemCommandHandler :  IRequestHandler<CreateGroceryListItemCommand, GetGroceryListItemDto>
{
    private readonly IMapper _mapper;
    private readonly IGroceryListItemRepository _groceryListItemRepository;
    public CreateGroceryListItemCommandHandler(IMapper mapper, IGroceryListItemRepository groceryListItemRepository)
    {
        _mapper = mapper;
        _groceryListItemRepository = groceryListItemRepository;
    }
    public async Task<GetGroceryListItemDto> Handle(CreateGroceryListItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateGroceryListItemValidator();
        var validationResults = await validator.ValidateAsync(request);

        if (validationResults.Errors.Any())
        {
            throw new BadRequestException("Invalid grocery list item", validationResults);
        }

        var entity = _mapper.Map<GroceryListItemEntity>(request);
        
        var dto =_mapper.Map<GetGroceryListItemDto>(await _groceryListItemRepository.CreateAsync(entity));
        
        return dto;
    }
}