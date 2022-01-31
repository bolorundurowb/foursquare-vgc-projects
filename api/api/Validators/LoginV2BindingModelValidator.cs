using api.Models.Binding;
using FluentValidation;

namespace api.Validators;

public class LoginV2BindingModelValidator : AbstractValidator<LoginV2BindingModel>
{
    public LoginV2BindingModelValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}