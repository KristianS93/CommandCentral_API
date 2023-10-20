using Application.Contracts.Household;
using Application.Features.Household.Shared;
using MediatR;

namespace Application.Features.Household.Queries.GetAllHouseholds;

public class GetAllHouseholdsQueryHandler : IRequestHandler<GetAllHouseholdsQuery, List<HouseholdDetailsDto>>
{
    private readonly IHouseholdRepository _householdRepository;
    public GetAllHouseholdsQueryHandler(IHouseholdRepository householdRepository)
    {
        _householdRepository = householdRepository;
    }
    public async Task<List<HouseholdDetailsDto>> Handle(GetAllHouseholdsQuery request, CancellationToken cancellationToken)
    {
        var households = await _householdRepository.GetAsync();

        var data = households.ToListOfDetailsDTO();

        return data;
    }
}