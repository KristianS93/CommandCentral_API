using Application.Contracts.Household;
using Application.Contracts.Identity;
using AutoMapper;
using Domain.Entities.Household;
using MediatR;

namespace Application.Features.Household.Queries.GetHousehold;

public class GetHouseholdQueryHandler : IRequestHandler<GetHouseholdQuery, HouseholdDto>
{
    private readonly IMapper _mapper;
    private readonly IHouseholdRepository _householdRepository;

    public GetHouseholdQueryHandler(IMapper mapper, IHouseholdRepository householdRepository)
    {
        _mapper = mapper;
        _householdRepository = householdRepository;
    }
    public async Task<HouseholdDto> Handle(GetHouseholdQuery request, CancellationToken cancellationToken)
    {
        // Get household id
        
        // Query database
        var household = await _householdRepository.GetByIdAsync(request.Id);
        
        // Verify record exist
        ArgumentNullException.ThrowIfNull(household);
        
        // Convert data object to DTO object
        var data = _mapper.Map<HouseholdDto>(household);

        return data;
    }
}