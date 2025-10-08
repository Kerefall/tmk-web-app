namespace TmkStoreAPI.Contracts
{
    public record RemnantRequest(
        Guid ID,
        Guid IDStock,
        decimal InStockT,
        decimal InStockM,
        decimal AvgTubeLength,
        decimal AvgTubeWeight);
}