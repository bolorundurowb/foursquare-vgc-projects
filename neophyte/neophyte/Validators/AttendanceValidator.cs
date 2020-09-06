using FluentValidation;
using neophyte.Models;

namespace neophyte.Validators
{
    public class AttendanceValidator : AbstractValidator<Attendance>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("The full name is missing");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("An email address is missing")
                .EmailAddress()
                .WithMessage("the email address is invalid");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("The phone number is missing");
        }
    }
}
