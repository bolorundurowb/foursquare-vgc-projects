using meerkat;
using meerkat.Attributes;
using MongoDB.Bson;

namespace neophyte.api.Data.Entities;

[Collection(TrackTimestamps = true), IgnoreUnknownFields]
public class OnlineAttendee : Schema
{
    public ObjectId PersonId { get; private set; }

    private OnlineAttendee()
    {
    }

    public OnlineAttendee(ObjectId personId) => PersonId = personId;
}