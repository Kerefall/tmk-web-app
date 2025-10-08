namespace TmkStore.DataAccess.Entities
{
    public class RemnantEntity
    {
        public Guid ID { get; set; }
        public Guid IDStock { get; set; }
        public decimal InStockT { get; set; }
        public decimal InStockM { get; set; }
        public decimal AvgTubeLength { get; set; }
        public decimal AvgTubeWeight { get; set; }
    }
}