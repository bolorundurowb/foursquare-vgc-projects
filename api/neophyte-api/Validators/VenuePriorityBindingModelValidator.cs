using api.Models.Binding;
using FluentValidation;

namespace neophyte;

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