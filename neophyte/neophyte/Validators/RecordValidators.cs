using FluentValidation;
using neophyte.Models;
using neophyte.Models.Binding;

namespace neophyte.Validators
{
    public class NewcomerValidator : AbstractValidator<NewcomerBindingModel>
    {
        public NewcomerValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("A full name is required.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("A phone number is required.")
                .Length(11)
                .WithMessage("A standard phone number should be 11 digits long.");
        }
    }
}