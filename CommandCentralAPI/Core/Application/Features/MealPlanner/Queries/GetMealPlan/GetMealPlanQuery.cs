using MediatR;

namespace Application.Features.MealPlanner.Queries.GetMealPlan;

public record GetMealPlanQuery : IRequest<MealPlanDto>;