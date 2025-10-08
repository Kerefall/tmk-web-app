namespace TmkStoreAPI.Contracts
{
    public record PriceRequest(
        Guid ID,
        Guid IDStock,
        decimal PriceT,
        decimal PriceLimitT1,
        decimal PriceT1,
        decimal PriceLimitT2,
        decimal PriceT2,
        decimal PriceM,
        decimal PriceLimitM1,
        decimal PriceM1,
        decimal PriceLimitM2,
        decimal PriceM2,
        decimal NDS);
}