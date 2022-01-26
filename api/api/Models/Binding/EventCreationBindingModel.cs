using System;
using System.Collections.Generic;

namespace api.Models.Binding;

public class EventCreationBindingModel
{
    public string Name { get; set; }

    public DateTime Date { get; set; }

    public List<VenuePriorityBindingModel> Venues { get; set; }
}