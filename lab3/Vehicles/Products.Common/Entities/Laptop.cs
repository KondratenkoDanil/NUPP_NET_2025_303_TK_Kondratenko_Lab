namespace Products.Common.Entities
{
    // Клас для ноутбуків, успадкований від Product
    public class Laptop : Product
    {
        public string Processor { get; set; }
        public int RAM { get; set; }

        // Конструктор класу Laptop
        public Laptop(Guid id, string name, decimal price, string description, string processor, int ram)
            : base(id, name, price, description)
        {
            Processor = processor;
            RAM = ram;
            ProductCount++;
        }

        public override string GetProductDetails()
        {
            return $"Ноутбук: {Name}, Процесор: {Processor}, ОЗП: {RAM} ГБ, Ціна: {Price:C}";
        }

        public static Laptop Create()
           => new Laptop(Guid.NewGuid(), string.Empty, 0, string.Empty, string.Empty, 0);

        public static Laptop Create(string name, decimal price, string description, string processor, int ram)
            => new Laptop(Guid.NewGuid(), name, price, description, processor, ram);
    }
}
