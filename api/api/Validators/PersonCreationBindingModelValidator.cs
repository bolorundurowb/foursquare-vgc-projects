using api.Models.Binding;
using FluentValidation;

namespace api.Validators;

public class PersonCreationBindingModelValidator : AbstractValidator<PersonCreationBindingModel>
{
    public PersonCreationBindingModelValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("A phone number is required.");
    }
}