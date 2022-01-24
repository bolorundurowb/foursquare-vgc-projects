using System.Collections.Generic;

namespace api.Models.View;

public class ValidationErrorsViewModel
{
    public IEnumerable<string> Messages { get; set; }

    public ValidationErrorsViewModel()
    {
    }

    public ValidationErrorsViewModel(IEnumerable<string> messages)
    {
        Messages = messages;
    }
}