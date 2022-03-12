using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class VenuePriorityBindingModelValidator : AbstractValidator<VenuePriorityBindingModel>
{
    public VenuePriorityBindingModelValidator()
    {
        RuleFor(x => x.Priority)
            .GreaterThan(0);

        RuleFor(x => x.VenueId)
            .NotEmpty();
    }
}