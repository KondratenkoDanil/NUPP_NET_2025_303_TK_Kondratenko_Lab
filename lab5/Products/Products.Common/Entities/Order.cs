using Products.Common.Contracts;

namespace Products.Common.Entities
{
    // Клас для замовлень
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice => CalculateTotalPrice();

        public delegate void OrderProcessedEventHandler(Order order);

        // Подія, яка викликається після обробки замовлення
        public event OrderProcessedEventHandler OrderProcessed;

        // Конструктор класу Order
        public Order(Guid id, Customer customer, List<Product> products, DateTime orderDate)
        {
            Id = id;
            Customer = customer;
            Products = products;
            OrderDate = orderDate;
        }

        // Метод для обчислення загальної вартості замовлення
        private decimal CalculateTotalPrice()
        {
            decimal total = 0;
            foreach (var product in Products)
            {
                total += product.Price;
            }
            return total;
        }

        // Метод для обробки замовлення та виклику події
        public void ProcessOrder()
        {
            Console.WriteLine($"Замовлення #{Id} оброблено.");
            OrderProcessed?.Invoke(this);
        }
    }
}