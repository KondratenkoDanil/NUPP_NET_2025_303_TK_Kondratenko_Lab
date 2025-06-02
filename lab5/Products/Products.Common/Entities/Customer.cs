using Products.Common.Contracts;

namespace Products.Common.Entities
{
    // Клас для клієнтів
    public class Customer : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Конструктор класу Customer
        public Customer(Guid id, string name, string email, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }

        public static Customer Create() 
            => new Customer(Guid.NewGuid(), string.Empty, string.Empty, string.Empty);
        
        public static Customer Create(string name, string email, string address)
            => new Customer(Guid.NewGuid(), name, email, address);
    }
}
