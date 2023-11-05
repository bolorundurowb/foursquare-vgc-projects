using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class RecordAttendeeBindingModelValidator : AbstractValidator<RecordAttendeeBindingModel>
{
    public RecordAttendeeBindingModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.EmailAddress)
            .EmailAddress()
            .When(x => x.EmailAddress != null);
    }
}
