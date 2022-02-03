using System;

namespace neophyte.api.Models.Binding;

public class NewcomerUpdateBindingModel : NewcomerBindingModel
{
    public string Remarks { get; set; }

    public DateTime? Date { get; set; }
}