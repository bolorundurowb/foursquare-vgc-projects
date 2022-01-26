using api.Models.Binding;
using FluentValidation;

namespace api.Validators;

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