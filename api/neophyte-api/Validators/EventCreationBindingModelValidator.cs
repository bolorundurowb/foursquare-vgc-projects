using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class EventCreationBindingModelValidator: AbstractValidator<EventCreationBindingModel>
{
    public EventCreationBindingModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Venues)
            .NotEmpty();

        RuleForEach(x => x.Venues)
            .SetValidator(new VenuePriorityBindingModelValidator());
    }
}