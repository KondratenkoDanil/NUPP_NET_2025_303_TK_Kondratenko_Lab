using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var teamService = new CrudService<F1Team>();

        var team1 = new F1Team { Name = "Mercedes", YearFounded = 1954, Driver = "Льюіс Хемілтон", CarModel = "W13" };
        var team2 = new F1Team { Name = "Red Bull Racing", YearFounded = 2005, Driver = "Макс Ферстаппен", CarModel = "RB18" };

        teamService.Create(team1);
        teamService.Create(team2);

        Console.WriteLine("Список команд Формули 1:");
        foreach (var team in teamService.ReadAll())
        {
            Console.WriteLine($"{team.Name} ({team.YearFounded}), Гонщик: {team.Driver}, Модель автомобіля: {team.CarModel}");
        }

        Console.WriteLine("\nПошук команди за ID першої команди:");
        var foundTeam = teamService.Read(team1.Id);
        if (foundTeam != null)
        {
            Console.WriteLine($"Знайдено: {foundTeam.Name}, Гонщик: {foundTeam.Driver}, Модель автомобіля: {foundTeam.CarModel}");
        }

        team1.CarModel = "W14"; // Оновлюємо модель автомобіля
        teamService.Update(team1);

        Console.WriteLine("\nОновлений список команд:");
        foreach (var team in teamService.ReadAll())
        {
            Console.WriteLine($"{team.Name} ({team.YearFounded}), Гонщик: {team.Driver}, Модель автомобіля: {team.CarModel}");
        }

        teamService.Remove(team2); // Видаляємо команду Red Bull Racing
        Console.WriteLine("\nСписок команд після видалення однієї:");
        foreach (var team in teamService.ReadAll())
        {
            Console.WriteLine($"{team.Name} ({team.YearFounded}), Гонщик: {team.Driver}, Модель автомобіля: {team.CarModel}");
        }

        Console.WriteLine("\nПрограма завершена. Натисніть Enter для виходу...");
        Console.ReadLine();
    }
}

// Клас команди Формули 1
class F1Team
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int YearFounded { get; set; }
    public string Driver { get; set; }
    public string CarModel { get; set; }
}

// Генерік CRUD-сервіс
class CrudService<T> where T : class
{
    private readonly List<T> _items = new();

    public void Create(T element) => _items.Add(element);
    public T Read(Guid id) => _items.FirstOrDefault(e => (e as dynamic).Id == id);
    public IEnumerable<T> ReadAll() => _items;
    public void Update(T element)
    {
        var existing = Read((element as dynamic).Id);
        if (existing != null)
        {
            _items.Remove(existing);
            _items.Add(element);
        }
    }
    public void Remove(T element) => _items.Remove(element);
}
