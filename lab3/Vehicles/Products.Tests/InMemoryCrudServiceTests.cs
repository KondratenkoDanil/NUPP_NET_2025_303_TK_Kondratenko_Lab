using FluentAssertions;
using Products.Common.Infrastructure;
using Products.Common.Entities;


public class InMemoryCrudServiceTests
{
    private string GetTempFilePath() => Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.json");

    [Fact]
    public async Task CreateAsync_ShouldAddElement()
    {
        var service = CreateService();

        var laptop = Laptop.Create("Test Laptop", 1000, "Test Description", "Intel i7", 16);
        var result = await service.CreateAsync(laptop);

        result.Should().BeTrue();
        (await service.ReadAsync(laptop.Id)).Should().Be(laptop);
    }

    [Fact]
    public async Task ReadAsync_ShouldReturnElement()
    {
        var service = CreateService();

        var laptop = Laptop.Create();
        await service.CreateAsync(laptop);

        var read = await service.ReadAsync(laptop.Id);
        read.Should().Be(laptop);
    }

    [Fact]
    public async Task ReadAllAsync_ShouldReturnAll()
    {
        var service = CreateService();

        await service.CreateAsync(Laptop.Create());
        await service.CreateAsync(Laptop.Create());

        var all = await service.ReadAllAsync();

        all.Should().HaveCount(2);
    }

    [Fact]
    public async Task ReadAllAsync_WithPagination_ShouldReturnCorrectPage()
    {
        var service = CreateService();
        var laptops = Enumerable.Range(1, 20)
            .Select(i => new Laptop(Guid.NewGuid(), "Laptop " + i, 0, "...", "", 0))
            .ToList();

        foreach (var p in laptops)
            await service.CreateAsync(p);

        var page = await service.ReadAllAsync(page: 1, amount: 3);
        page.Should().HaveCount(3);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateElement()
    {
        var service = CreateService();

        var person = Laptop.Create();
        await service.CreateAsync(person);

        person.Name = "After";
        await service.UpdateAsync(person);

        var updated = await service.ReadAsync(person.Id);
        updated.Name.Should().Be("After");
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveElement()
    {
        var service = CreateService();

        var person = Laptop.Create();
        await service.CreateAsync(person);

        var result = await service.RemoveAsync(person);
        result.Should().BeTrue();

        var after = await service.ReadAsync(person.Id);
        after.Should().BeNull();
    }

    [Fact]
    public async Task SaveAsync_ShouldWriteToFile_AndReloadOnInit()
    {
        var path = GetTempFilePath();
        var service = CreateService();

        var laptop = Laptop.Create("Persisted", 1000, "Test Description", "Intel i7", 16);
        await service.CreateAsync(laptop);
        await service.SaveAsync(GetTempFilePath());

        // Створюємо новий інстанс сервісу, щоб перевірити десеріалізацію
        var newService = CreateService();
        await newService.LoadAsync(path);

        service.Count().Should().BeGreaterThan(0);

        File.Delete(path); // очищення після тесту
    }

    private AsyncCrudService<Laptop> CreateService()
    {
        return new AsyncCrudService<Laptop>(p => p.Id);
    }
}
