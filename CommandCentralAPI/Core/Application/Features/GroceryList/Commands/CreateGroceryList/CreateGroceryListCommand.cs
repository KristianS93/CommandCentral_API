using MediatR;

namespace Application.Features.GroceryList.Commands.CreateGroceryList;

public record CreateGroceryListCommand(int HouseholdId) : IRequest<CreateGroceryListDto>;