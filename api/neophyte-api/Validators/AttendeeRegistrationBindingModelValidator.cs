using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class AttendeeRegistrationBindingModelValidator : AbstractValidator<AttendeeRegistrationBindingModel>
{
    public AttendeeRegistrationBindingModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("A full name is required.");
    }
}