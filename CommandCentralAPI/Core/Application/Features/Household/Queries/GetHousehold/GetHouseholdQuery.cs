using MediatR;

namespace Application.Features.Household.Queries.GetHousehold;

public record GetHouseholdQuery(int Id) : IRequest<HouseholdDto>;