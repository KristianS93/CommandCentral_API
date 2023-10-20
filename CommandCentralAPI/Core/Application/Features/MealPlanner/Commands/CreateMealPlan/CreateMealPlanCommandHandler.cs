using MediatR;

namespace Application.Features.MealPlanner.Commands.CreateMealPlan;

public class CreateMealPlanCommandHandler : IRequestHandler<CreateMealPlanCommand, Unit>
{
    public Task<Unit> Handle(CreateMealPlanCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}