using Application.Contracts.Household;
using MediatR;

namespace Application.Features.Household.Commands.DeleteHousehold;

public class DeleteHouseholdCommandHandler : IRequestHandler<DeleteHouseholdCommand, Unit>
{
    private readonly IHouseholdRepository _householdRepository;

    public DeleteHouseholdCommandHandler(IHouseholdRepository householdRepository)
    {
        _householdRepository = householdRepository;
    }
    public async Task<Unit> Handle(DeleteHouseholdCommand request, CancellationToken cancellationToken)
    {
        var household = await _householdRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(household);

        await _householdRepository.DeleteAsync(household);
        
        return Unit.Value;
    }
}