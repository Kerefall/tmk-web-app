namespace TmkStoreAPI.Contracts
{
    public record RemnantResponse(
        Guid ID,
        Guid IDStock,
        decimal InStockT,
        decimal InStockM,
        decimal AvgTubeLength,
        decimal AvgTubeWeight);
}