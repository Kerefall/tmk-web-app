namespace TmkStore.Core.Models
{
    public class Remnant
    {
        private Remnant(
            Guid id,
            Guid idStock,
            decimal inStockT,
            decimal inStockM,
            decimal avgTubeLength,
            decimal avgTubeWeight)
        {
            ID = id;
            IDStock = idStock;
            InStockT = inStockT;
            InStockM = inStockM;
            AvgTubeLength = avgTubeLength;
            AvgTubeWeight = avgTubeWeight;
        }

        public Guid ID { get; }
        public Guid IDStock { get; }
        public decimal InStockT { get; }
        public decimal InStockM { get; }
        public decimal AvgTubeLength { get; }
        public decimal AvgTubeWeight { get; }

        public static (Remnant Remnant, string Error) Create(
            Guid id,
            Guid idStock,
            decimal inStockT = 0,
            decimal inStockM = 0,
            decimal avgTubeLength = 0,
            decimal avgTubeWeight = 0)
        {
            var error = string.Empty;

            // Проверка на отрицательные остатки
            if (inStockT < 0 || inStockM < 0)
            {
                error = "Остатки не могут быть отрицательными";
            }

            // Проверка на отрицательные шаги
            if (avgTubeLength < 0 || avgTubeWeight < 0)
            {
                error = "Шаги длины и веса не могут быть отрицательными";
            }

            var remnant = new Remnant(
                id,
                idStock,
                inStockT,
                inStockM,
                avgTubeLength,
                avgTubeWeight);

            return (remnant, error);
        }
    }
}