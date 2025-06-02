
namespace Products.Common.Contracts;


// Інтерфейс для сутностей, які будуть зберігатися в сервісі
public interface IEntity
{
    Guid Id { get; set; }
}
