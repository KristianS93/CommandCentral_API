using MediatR;

namespace Application.Features.Household.Commands.DeleteHousehold;

public class DeleteHouseholdCommand : IRequest<Unit>
{
    public int Id { get; set; }
    
}