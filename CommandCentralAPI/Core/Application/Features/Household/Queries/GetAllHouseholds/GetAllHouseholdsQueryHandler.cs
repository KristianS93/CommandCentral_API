using Application.Contracts.Household;
using Application.Features.Household.Shared;
using AutoMapper;
using MediatR;

namespace Application.Features.Household.Queries.GetAllHouseholds;

public class GetAllHouseholdsQueryHandler : IRequestHandler<GetAllHouseholdsQuery, List<HouseholdDetailsDto>>
{
    private readonly IMapper _mapper;
    private readonly IHouseholdRepository _householdRepository;
    public GetAllHouseholdsQueryHandler(IMapper mapper, IHouseholdRepository householdRepository)
    {
        _mapper = mapper;
        _householdRepository = householdRepository;
    }
    public async Task<List<HouseholdDetailsDto>> Handle(GetAllHouseholdsQuery request, CancellationToken cancellationToken)
    {
        var households = await _householdRepository.GetAsync();

        var data = _mapper.Map<List<HouseholdDetailsDto>>(households);

        return data;
    }
}