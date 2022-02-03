using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

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