using api.Models.Binding;
using FluentValidation;

namespace neophyte;

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