using FluentValidation;
using MongoDB.Bson;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class SeatAssignmentBindingModelValidator : AbstractValidator<SeatAssignmentBindingModel>
{
    public SeatAssignmentBindingModelValidator()
    {
        RuleFor(x => x.Category)
            .IsInEnum();

        RuleFor(x => x.PersonId)
            .NotEmpty()
            .Must(x => ObjectId.TryParse(x, out _));
    }
}