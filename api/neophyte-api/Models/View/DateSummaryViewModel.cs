using System;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class DateSummaryViewModel
{
    /// <summary>
    /// The attendance date
    /// </summary>
    /// <example>2022-02-04T00:00:00.000Z</example>
    [SwaggerSchema(Nullable = false)]
    public DateTime Date { get; set; }

    /// <summary>
    /// A human readable representation of the attendance date
    /// </summary>
    /// <example>Tue, 04 Jun 2022</example>
    [SwaggerSchema(Nullable = false)]
    public string HumanReadableDate { get; set; }

    /// <summary>
    /// The number of attendees for the specified date
    /// </summary>
    /// <example>18</example>
    [SwaggerSchema(Nullable = false)]
    public int NumOfEntries { get; set; }
}