using FluentValidation;
using neophyte.Models.Binding;

namespace neophyte.Validators
{
    public class AttendanceValidator : AbstractValidator<AttendeeBindingModel>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("The full name is missing");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("The phone number is missing");
        }
    }
}
