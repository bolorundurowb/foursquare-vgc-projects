using api.Models.Binding;
using FluentValidation;

namespace neophyte;

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