using System.Text.Json.Serialization;
using MediatR;

namespace Application.Features.Household.Commands.UpdateHousehold;

public class UpdateHouseholdCommand : IRequest<Unit>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}