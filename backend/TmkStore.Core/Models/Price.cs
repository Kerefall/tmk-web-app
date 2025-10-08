namespace TmkStore.Core.Models
{
    public class Price
    {
        private Price(
            Guid id,
            Guid idStock,
            decimal priceT,
            decimal priceLimitT1,
            decimal priceT1,
            decimal priceLimitT2,
            decimal priceT2,
            decimal priceM,
            decimal priceLimitM1,
            decimal priceM1,
            decimal priceLimitM2,
            decimal priceM2,
            decimal nds)
        {
            ID = id;
            IDStock = idStock;
            PriceT = priceT;
            PriceLimitT1 = priceLimitT1;
            PriceT1 = priceT1;
            PriceLimitT2 = priceLimitT2;
            PriceT2 = priceT2;
            PriceM = priceM;
            PriceLimitM1 = priceLimitM1;
            PriceM1 = priceM1;
            PriceLimitM2 = priceLimitM2;
            PriceM2 = priceM2;
            NDS = nds;
        }

        public Guid ID { get; }
        public Guid IDStock { get; }
        public decimal PriceT { get; }
        public decimal PriceLimitT1 { get; }
        public decimal PriceT1 { get; }
        public decimal PriceLimitT2 { get; }
        public decimal PriceT2 { get; }
        public decimal PriceM { get; }
        public decimal PriceLimitM1 { get; }
        public decimal PriceM1 { get; }
        public decimal PriceLimitM2 { get; }
        public decimal PriceM2 { get; }
        public decimal NDS { get; }

        public static (Price Price, string Error) Create(
            Guid id,
            Guid idStock,
            decimal priceT = 0,
            decimal priceLimitT1 = 0,
            decimal priceT1 = 0,
            decimal priceLimitT2 = 0,
            decimal priceT2 = 0,
            decimal priceM = 0,
            decimal priceLimitM1 = 0,
            decimal priceM1 = 0,
            decimal priceLimitM2 = 0,
            decimal priceM2 = 0,
            decimal nds = 0)
        {
            var error = string.Empty;

            // Проверка на отрицательные цены
            if (priceT < 0 || priceT1 < 0 || priceT2 < 0 || priceM < 0 || priceM1 < 0 || priceM2 < 0)
            {
                error = "Цены не могут быть отрицательными";
            }

            // Проверка на отрицательные лимиты
            if (priceLimitT1 < 0 || priceLimitT2 < 0 || priceLimitM1 < 0 || priceLimitM2 < 0)
            {
                error = "Лимиты объемов не могут быть отрицательными";
            }

            // Проверка НДС (обычно от 0 до 100)
            if (nds < 0 || nds > 100)
            {
                error = "НДС должен быть в диапазоне от 0 до 100";
            }

            var price = new Price(
                id,
                idStock,
                priceT,
                priceLimitT1,
                priceT1,
                priceLimitT2,
                priceT2,
                priceM,
                priceLimitM1,
                priceM1,
                priceLimitM2,
                priceM2,
                nds);

            return (price, error);
        }
    }
}