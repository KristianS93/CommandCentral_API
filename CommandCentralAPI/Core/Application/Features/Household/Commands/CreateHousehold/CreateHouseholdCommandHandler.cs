using Application.Contracts.Household;
using Application.Exceptions;
using Application.Features.Household.Shared;
using MediatR;

namespace Application.Features.Household.Commands.CreateHousehold;

public class CreateHouseholdCommandHandler : IRequestHandler<CreateHouseholdCommand, HouseholdDetailsDto>
{
    private readonly IHouseholdRepository _householdRepository;
    public CreateHouseholdCommandHandler(IHouseholdRepository householdRepository)
    {
        _householdRepository = householdRepository;
    }
    public async Task<HouseholdDetailsDto> Handle(CreateHouseholdCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateHouseholdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Household", validationResult);
        }

        var household = request.ToHouseholdEntity();
        var dto = (await _householdRepository.CreateAsync(household)).ToDetailsDTO();
        
        return dto;
    }
}