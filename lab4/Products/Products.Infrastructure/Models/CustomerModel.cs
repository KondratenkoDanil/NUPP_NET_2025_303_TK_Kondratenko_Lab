using System.ComponentModel.DataAnnotations;

namespace Products.Infrastructure.Models;

// Клас для клієнтів
public class CustomerModel
{
    [Key]
    public Guid Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    [Required, StringLength(100), EmailAddress]
    public string Email { get; set; }

    [Required, StringLength(100)]
    public string Address { get; set; }
}
