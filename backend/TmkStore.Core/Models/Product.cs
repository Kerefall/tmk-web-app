namespace TmkStore.Core.Models
{
    public class Product
    {
        public const int MAX_TITLE_LENGTH = 250;
        private Product(Guid id, string title, string description, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }

        public Guid Id { get; }

        public string Title { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public decimal Price { get; }

        public static (Product Product, string Error) Create(Guid id, string title, string description, decimal price)
        {
            var error = string.Empty;

            if (String.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Название продукта не может быть пустым или длиннее 250 символов";
            }

            var product = new Product(id, description, title, price);

            return (product, error);
        }
    }
}
