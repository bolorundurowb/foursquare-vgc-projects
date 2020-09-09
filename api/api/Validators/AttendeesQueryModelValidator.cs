using System;
using api.Models.Binding;
using FluentValidation;

namespace api.Validators
{
    public class AttendeesQueryModelValidator : AbstractValidator<AttendeesQueryModel>
    {
        public AttendeesQueryModelValidator()
        {
            RuleFor(x => x.Date)
                .NotEqual(DateTime.MinValue)
                .WithMessage("Invalid date")
                .NotEqual(DateTime.MaxValue)
                .WithMessage("Invalid date");
        }
    }
}