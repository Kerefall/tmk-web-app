namespace TmkStore.DataAccess.Entities
{
    public class StockEntity
    {
        public Guid IDStock { get; set; }
        public string City { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
    }
}