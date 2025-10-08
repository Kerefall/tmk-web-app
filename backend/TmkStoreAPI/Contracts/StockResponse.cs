namespace TmkStoreAPI.Contracts
{
    public record StockResponse(
        Guid IDStock,
        string City,
        string StockName);
}