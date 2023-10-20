using Application.Features.Household.Shared;
using MediatR;

namespace Application.Features.Household.Queries.GetAllHouseholds;

public record GetAllHouseholdsQuery : IRequest<List<HouseholdDetailsDto>>;
