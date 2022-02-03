using api.Models.Binding;
using FluentValidation;

namespace neophyte;

public class VenueCreationBindingModelValidator : AbstractValidator<VenueCreationBindingModel>
{
    public VenueCreationBindingModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.SeatRanges)
            .NotEmpty();

        RuleForEach(x => x.SeatRanges)
            .SetValidator(new SeatRangeBindingModelValidator());
    }
}