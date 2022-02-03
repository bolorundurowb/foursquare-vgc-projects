using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

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