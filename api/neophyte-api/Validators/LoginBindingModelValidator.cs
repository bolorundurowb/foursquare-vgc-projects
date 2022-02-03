using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class LoginBindingModelValidator : AbstractValidator<LoginBindingModel>
{
    public LoginBindingModelValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Your email address is required.")
            .EmailAddress()
            .WithMessage("EmailAddress is invalid.");
    }
}