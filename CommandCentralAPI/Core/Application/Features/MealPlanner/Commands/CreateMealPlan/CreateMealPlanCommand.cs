using MediatR;

namespace Application.Features.MealPlanner.Commands.CreateMealPlan;

public record CreateMealPlanCommand() : IRequest<Unit>;