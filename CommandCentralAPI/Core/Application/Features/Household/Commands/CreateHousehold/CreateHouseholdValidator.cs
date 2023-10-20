using FluentValidation;

namespace Application.Features.Household.Commands.CreateHousehold;

public class CreateHouseholdValidator : AbstractValidator<CreateHouseholdCommand>
{
    public CreateHouseholdValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50 characters")
            .MinimumLength(3).WithMessage("{PropertyName} must be more than 3 characters");
    }
}