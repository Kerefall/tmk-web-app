namespace TmkStore.Core.Models
{
    public class Stock
    {
        public const int MAX_CITY_LENGTH = 100;
        public const int MAX_STOCK_NAME_LENGTH = 250;

        private Stock(Guid idStock, string city, string stockName)
        {
            IDStock = idStock;
            City = city;
            StockName = stockName;
        }

        public Guid IDStock { get; }
        public string City { get; } = string.Empty;
        public string StockName { get; } = string.Empty;

        public static (Stock Stock, string Error) Create(Guid idStock, string city, string stockName)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(city) || city.Length > MAX_CITY_LENGTH)
            {
                error = "Город склада не может быть пустым или длиннее 100 символов";
            }
            else if (string.IsNullOrEmpty(stockName) || stockName.Length > MAX_STOCK_NAME_LENGTH)
            {
                error = "Название склада не может быть пустым или длиннее 250 символов";
            }

            var stock = new Stock(idStock, city, stockName);
            return (stock, error);
        }
    }
}