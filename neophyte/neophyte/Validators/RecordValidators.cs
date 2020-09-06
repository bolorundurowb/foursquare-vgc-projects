using FluentValidation;
using neophyte.Models;

namespace neophyte.Validators
{
    public class RecordValidator : AbstractValidator<Record>
    {
        public RecordValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Phone)
                .NotEmpty()
                .Length(11);
        }
    }
}
