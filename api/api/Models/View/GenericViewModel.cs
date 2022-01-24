namespace api.Models.View;

public class GenericViewModel
{
    public string Message { get; set; }

    public GenericViewModel()
    {
    }

    public GenericViewModel(string message)
    {
        Message = message;
    }
}