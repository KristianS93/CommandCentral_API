using Application.Features.Household.Shared;
using MediatR;

namespace Application.Features.Household.Commands.CreateHousehold;

public class CreateHouseholdCommand : IRequest<HouseholdDetailsDto>
{
    public string Name { get; set; } = String.Empty;
}