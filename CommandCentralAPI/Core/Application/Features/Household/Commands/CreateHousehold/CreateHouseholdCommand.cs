using System.Text.Json.Serialization;
using Application.Features.Household.Shared;
using MediatR;

namespace Application.Features.Household.Commands.CreateHousehold;

public class CreateHouseholdCommand : IRequest<HouseholdDetailsDto>
{
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
}