using System;
using meerkat;
using meerkat.Attributes;

namespace api.Data.Entities;

[Collection(TrackTimestamps = true)]
public class Event : Schema
{
    public string Name { get; private set; }
    
    public DateOnly Date { get; private set; }
    
    public DateOnly Date { get; private set; }
}