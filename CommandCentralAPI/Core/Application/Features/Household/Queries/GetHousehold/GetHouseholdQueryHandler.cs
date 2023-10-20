using Application.Contracts.Household;
using MediatR;

namespace Application.Features.Household.Queries.GetHousehold;

public class GetHouseholdQueryHandler : IRequestHandler<GetHouseholdQuery, HouseholdDto>
{
    private readonly IHouseholdRepository _householdRepository;

    public GetHouseholdQueryHandler(IHouseholdRepository householdRepository)
    {
        _householdRepository = householdRepository;
    }
    public async Task<HouseholdDto> Handle(GetHouseholdQuery request, CancellationToken cancellationToken)
    {
        // Query database
        var household = await _householdRepository.GetByIdAsync(request.Id);
        
        // Verify record exist
        ArgumentNullException.ThrowIfNull(household);
        
        // Convert data object to DTO object
        var data = household.ToDTO();

        return data;
    }
}