using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class SeatChangeBindingModelValidator : AbstractValidator<SeatChangeBindingModel>
{
    public SeatChangeBindingModelValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty();

        RuleFor(x => x.SeatNumber)
            .NotEmpty();
    }
}