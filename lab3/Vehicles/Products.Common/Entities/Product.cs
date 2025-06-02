using Products.Common.Contracts;

namespace Products.Common.Entities
{
    // Базовий клас для всіх товарів
    public abstract class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Product(Guid id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }

        // Абстрактний метод для отримання детальної інформації про товар
        public abstract string GetProductDetails();

        // Статичне поле для зберігання кількості створених товарів
        public static int ProductCount { get; set; }

        // Статичний конструктор класу Product
        static Product()
        {
            ProductCount = 0;
        }

        // Статичний метод для отримання кількості створених товарів
        public static int GetTotalProductsCount()
        {
            return ProductCount;
        }
    }
}
