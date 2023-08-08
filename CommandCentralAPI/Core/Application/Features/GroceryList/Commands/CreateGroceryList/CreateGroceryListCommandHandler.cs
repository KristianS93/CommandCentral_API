using Application.Contracts.GroceryList;
using AutoMapper;
using Domain.Entities.GroceryList;
using MediatR;

namespace Application.Features.GroceryList.Commands.CreateGroceryList;

public class CreateGroceryListCommandHandler : IRequestHandler<CreateGroceryListCommand, CreateGroceryListDto>
{
    private readonly IGroceryListRepository _groceryListRepository;
    private readonly IMapper _mapper;

    public CreateGroceryListCommandHandler(IGroceryListRepository groceryListRepository, IMapper mapper)
    {
        _groceryListRepository = groceryListRepository;
        _mapper = mapper;
    }
    public async Task<CreateGroceryListDto> Handle(CreateGroceryListCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<GroceryListEntity>(request);
        var result = await _groceryListRepository.CreateAsync(entity);
        
        return _mapper.Map<CreateGroceryListDto>(result);
    }
}