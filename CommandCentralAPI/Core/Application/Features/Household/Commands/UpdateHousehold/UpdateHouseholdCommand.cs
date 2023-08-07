using MediatR;

namespace Application.Features.Household.Commands.UpdateHousehold;

public class UpdateHouseholdCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
}