using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class CreateAdminBindingModelValidator : AbstractValidator<CreateAdminBindingModel>
{
    public CreateAdminBindingModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}