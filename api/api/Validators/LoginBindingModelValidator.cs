using api.Models.Binding;
using FluentValidation;

namespace api.Validators
{
    public class LoginBindingModelValidator : AbstractValidator<LoginBindingModel>
    {
        public LoginBindingModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Your email address is required.")
                .EmailAddress()
                .WithMessage("Email is invalid.");
        }
    }
}