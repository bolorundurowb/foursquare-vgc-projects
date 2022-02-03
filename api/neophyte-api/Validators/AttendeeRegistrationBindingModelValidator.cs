using api.Models.Binding;
using FluentValidation;

namespace neophyte;

public class AttendeeRegistrationBindingModelValidator : AbstractValidator<AttendeeRegistrationBindingModel>
{
    public AttendeeRegistrationBindingModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("A full name is required.");
    }
}