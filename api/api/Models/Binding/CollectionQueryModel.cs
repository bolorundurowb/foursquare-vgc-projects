using api.Shared;

namespace api.Models.Binding;

public class CollectionQueryModel
{
    public int? Skip { get; set; } = 0;

    public int? Limit { get; set; } = Constants.MaxItemsPerPage;
}