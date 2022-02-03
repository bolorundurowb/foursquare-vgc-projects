using api.Models.Binding;
using FluentValidation;
using MongoDB.Bson;

namespace neophyte;

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