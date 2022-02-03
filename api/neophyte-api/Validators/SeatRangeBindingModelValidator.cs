using api.Models.Binding;
using FluentValidation;

namespace neophyte;

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