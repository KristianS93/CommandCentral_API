using Application.Contracts.Household;
using Application.Exceptions;
using Application.Features.Household.Commands.CreateHousehold;
using AutoMapper;
using Domain.Entities.Household;
using MediatR;

namespace Application.Features.Household.Commands.UpdateHousehold;

public class UpdateHouseholdCommandHandler : IRequestHandler<UpdateHouseholdCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IHouseholdRepository _householdRepository;

    public UpdateHouseholdCommandHandler(IMapper mapper, IHouseholdRepository householdRepository)
    {
        _mapper = mapper;
        _householdRepository = householdRepository;
    }
    public async Task<Unit> Handle(UpdateHouseholdCommand request, CancellationToken cancellationToken)
    {
        // validation of incoming data
        var validator = new UpdateHouseholdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid household", validationResult);
        }
    
        var householdData = await _householdRepository.GetByIdAsync(request.Id);
        if (householdData == null)
        {
            throw new NotFoundException(nameof(HouseholdEntity), request.Id);
        }
        
        
        // This convert keeps the fields not updated
        _mapper.Map(request, householdData);
        // this works because of the map have migrated the new data
        await _householdRepository.UpdateAsync(householdData);
        
        return Unit.Value;
    }
}