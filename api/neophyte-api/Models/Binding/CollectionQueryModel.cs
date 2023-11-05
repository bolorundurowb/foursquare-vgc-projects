using neophyte.api.Shared;

namespace neophyte.api.Models.Binding;

public class CollectionQueryModel
{
    /// <summary>
    /// The number of items to skip. It defaults to 0
    /// </summary>
    /// <example>10</example>
    public int Skip { get; set; } = 0;

    /// <summary>
    /// The max number of items to return. It defaults to 35
    /// </summary>
    /// <example>20</example>
    public int Limit { get; set; } = Constants.MaxItemsPerPage;
}