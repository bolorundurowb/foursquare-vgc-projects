using api.Models.Binding;
using FluentValidation;

namespace api.Validators;

public class SeatRangeBindingModelValidator : AbstractValidator<SeatRangeBindingModel>
{
    public SeatRangeBindingModelValidator()
    {
        RuleFor(x => x.Category)
            .IsInEnum();

        RuleFor(x => x.Number)
            .NotEmpty();
    }
}