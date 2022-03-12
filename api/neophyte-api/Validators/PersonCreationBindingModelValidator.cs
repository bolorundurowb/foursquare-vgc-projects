using FluentValidation;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class PersonCreationBindingModelValidator : AbstractValidator<PersonCreationBindingModel>
{
    public PersonCreationBindingModelValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("A phone number is required.");
    }
}