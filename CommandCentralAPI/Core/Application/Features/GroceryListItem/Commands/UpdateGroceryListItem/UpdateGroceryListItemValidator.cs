using FluentValidation;

namespace Application.Features.GroceryListItem.Commands.UpdateGroceryListItem;

public class UpdateGroceryListItemValidator : AbstractValidator<UpdateGroceryListItemCommand>
{
    public UpdateGroceryListItemValidator()
    {
        RuleFor(p => p.ItemName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MinimumLength(1).WithMessage("{PropertyName} must be more than 3 characters.")
            .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters.");

        RuleFor(p => p.ItemAmount)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} amount must be 0 or more.");
    }
}