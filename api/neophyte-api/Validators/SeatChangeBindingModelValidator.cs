using FluentValidation;
using MongoDB.Bson;
using neophyte.api.Models.Binding;

namespace neophyte.api.Validators;

public class SeatChangeBindingModelValidator : AbstractValidator<SeatChangeBindingModel>
{
    public SeatChangeBindingModelValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty()
            .Must(x => ObjectId.TryParse(x, out _))
            .WithMessage("Invalid person id");

        RuleFor(x => x.VenueId)
            .NotEmpty()
            .Must(x => ObjectId.TryParse(x, out _))
            .WithMessage("Invalid venue id");

        RuleFor(x => x.SeatNumber)
            .NotEmpty();
    }
}