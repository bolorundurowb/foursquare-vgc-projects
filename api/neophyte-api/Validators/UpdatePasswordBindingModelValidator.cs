using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

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