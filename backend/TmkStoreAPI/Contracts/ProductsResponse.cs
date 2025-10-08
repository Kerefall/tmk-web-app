namespace TmkStoreAPI.Contracts
{
    public record ProductsResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price);

    public record ProductsRequest(
        string Title,
        string Description,
        decimal Price);
}
