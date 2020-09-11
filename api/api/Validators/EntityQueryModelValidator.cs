using System;
using api.Models.Binding;
using FluentValidation;

namespace api.Validators
{
    public class EntityQueryModelValidator : AbstractValidator<EntityQueryModel>
    {
        public EntityQueryModelValidator()
        {
            RuleFor(x => x.Date)
                .NotEqual(DateTime.MinValue)
                .WithMessage("Invalid date")
                .NotEqual(DateTime.MaxValue)
                .WithMessage("Invalid date");
        }
    }
}
