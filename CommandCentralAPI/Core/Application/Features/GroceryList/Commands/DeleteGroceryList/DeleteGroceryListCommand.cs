using MediatR;

namespace Application.Features.GroceryList.Commands.DeleteGroceryList;

public record DeleteGroceryListCommand(int ListId, int HouseholdId) : IRequest<Unit>;
