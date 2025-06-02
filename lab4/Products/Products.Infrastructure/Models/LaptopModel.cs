using System.ComponentModel.DataAnnotations;

namespace Products.Infrastructure.Models;

// Клас для ноутбуків, успадкований від Product
public class LaptopModel
{
    [Key]
    public Guid Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required, StringLength(100)]
    public string Description { get; set; }

    [Required, StringLength(100)]
    public string Processor { get; set; }

    [Required]
    public int RAM { get; set; }

    public ICollection<OrderModel> Orders { get; set; }
}
