using Application.Contracts.Household;
using Application.Exceptions;
using Application.Features.Household.Shared;
using AutoMapper;
using Domain.Entities.Household;
using MediatR;

namespace Application.Features.Household.Commands.CreateHousehold;

public class CreateHouseholdCommandHandler : IRequestHandler<CreateHouseholdCommand, HouseholdDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IHouseholdRepository _householdRepository;
    public CreateHouseholdCommandHandler(IMapper mapper, IHouseholdRepository householdRepository)
    {
        _mapper = mapper;
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

        var household = _mapper.Map<HouseholdEntity>(request);
        var dto = _mapper.Map<HouseholdDetailsDto>(await _householdRepository.CreateAsync(household));
        
        return dto;
    }
}