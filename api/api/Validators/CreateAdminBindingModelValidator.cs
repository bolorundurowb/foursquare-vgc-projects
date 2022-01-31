using api.Models.Binding;
using FluentValidation;

namespace api.Validators;

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