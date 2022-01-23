using System;
using meerkat;
using meerkat.Attributes;

namespace api.Data.Entities;

[Collection(TrackTimestamps = true)]
public class Event : Schema
{
    public string Name { get; private set; }
    
    public DateOnly Date { get; private set; }
    
    public string Url { get; private set; }
    
    public string QrBase64 { get; private set; }
}