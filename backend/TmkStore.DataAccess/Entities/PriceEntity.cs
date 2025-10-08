namespace TmkStore.DataAccess.Entities
{
    public class PriceEntity
    {
        public Guid ID { get; set; }
        public Guid IDStock { get; set; }
        public decimal PriceT { get; set; }
        public decimal PriceLimitT1 { get; set; }
        public decimal PriceT1 { get; set; }
        public decimal PriceLimitT2 { get; set; }
        public decimal PriceT2 { get; set; }
        public decimal PriceM { get; set; }
        public decimal PriceLimitM1 { get; set; }
        public decimal PriceM1 { get; set; }
        public decimal PriceLimitM2 { get; set; }
        public decimal PriceM2 { get; set; }
        public decimal NDS { get; set; }
    }
}