using System.ComponentModel.DataAnnotations;

namespace Products.Infrastructure.Models;

// Клас для замовлень
public class OrderModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public CustomerModel Customer { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public ICollection<LaptopModel> Products { get; set; }

    public decimal TotalPrice => CalculateTotalPrice();

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
}