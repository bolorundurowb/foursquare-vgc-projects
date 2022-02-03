using api.Models.Binding;
using FluentValidation;

namespace neophyte;

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