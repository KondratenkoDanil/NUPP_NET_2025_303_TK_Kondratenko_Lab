namespace Products.Common.Entities
{
    // Клас для смартфонів, успадкований від Product
    public class Smartphone : Product
    {
        public string OperatingSystem { get; set; }
        public int Memory { get; set; }

        // Конструктор класу Smartphone
        public Smartphone(Guid id, string name, decimal price, string description, string os, int memory)
            : base(id, name, price, description)
        {
            OperatingSystem = os;
            Memory = memory;
            ProductCount++;
        }

        public override string GetProductDetails()
        {
            return $"Смартфон: {Name}, ОС: {OperatingSystem}, Пам'ять: {Memory} ГБ, Ціна: {Price:C}";
        }

        public static Smartphone Create()
            => new Smartphone(Guid.NewGuid(), string.Empty, 0, string.Empty, string.Empty, 0);

        public static Smartphone Create(string name, decimal price, string description, string os, int memory)
            => new Smartphone(Guid.NewGuid(), name, price, description, os, memory);
    }
}