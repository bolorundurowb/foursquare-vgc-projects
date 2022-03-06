using FluentValidation;
using MongoDB.Bson;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class OnlineRegistrationBindingModelValidator : AbstractValidator<OnlineRegistrationBindingModel>
{
    public OnlineRegistrationBindingModelValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty()
            .Must(x => ObjectId.TryParse(x, out _));
    }
}