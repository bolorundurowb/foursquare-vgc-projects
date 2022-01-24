using api.Models.Binding;
using FluentValidation;

namespace api.Validators;

public class AttendeeRegistrationBindingModelValidator : AbstractValidator<AttendeeRegistrationBindingModel>
{
    public AttendeeRegistrationBindingModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("A full name is required.");
    }
}