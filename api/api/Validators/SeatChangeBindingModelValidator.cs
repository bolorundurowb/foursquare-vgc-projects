using api.Models.Binding;
using FluentValidation;

namespace api.Validators;

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