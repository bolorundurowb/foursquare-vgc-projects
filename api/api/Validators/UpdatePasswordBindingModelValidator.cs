using api.Models.Binding;
using FluentValidation;

namespace api.Validators;

public class UpdatePasswordBindingModelValidator : AbstractValidator<UpdatePasswordBindingModel>
{
    public UpdatePasswordBindingModelValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password);
    }
}