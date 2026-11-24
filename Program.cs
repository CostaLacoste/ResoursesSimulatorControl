using System;

namespace ResManSim;

struct ResourceBundle
{
    public int Wood { get; }
    public int Stone { get; }
    public int Food { get; }

    public ResourceBundle(int wood, int stone, int food)
    {
        Wood = wood;
        Stone = stone;
        Food = food;
    }

    // Возвращает новый набор ресурсов, не изменяя исходные значения
    public ResourceBundle Add(ResourceBundle other)
    {
        return new ResourceBundle(
            Wood + other.Wood,
            Stone + other.Stone,
            Food + other.Food
        );
    }

    public override string ToString()
    {
        return $"Дерево: {Wood}, Камень: {Stone}, Еда: {Food}";
    }
}

interface IUpgradable
{
    int Level { get; }
    ResourceBundle GetUpgradeCost();
    void Upgrade();
}

abstract class Building : IUpgradable
{
    public string Name { get; protected set; }

    private int level;

    public int Level
    {
        get => level;
        protected set => level = Math.Max(1, value); // Уровень не ниже 1
    }

    protected Building(string name, int initialLevel = 1)
    {
        Name = name;
        Level = initialLevel;
    }

    // Каждое здание само определяет, сколько и каких ресурсов добывать
    public abstract ResourceBundle Produce();

    public abstract ResourceBundle GetUpgradeCost();

    public virtual void Upgrade()
    {
        Level++;
    }
}

class Program
{
    static void Main()
    {
        ResourceBundle startResources = new ResourceBundle(10, 5, 5);
        ResourceBundle dailyIncome = new ResourceBundle(3, 2, 4);
        ResourceBundle total = startResources.Add(dailyIncome);

        Console.WriteLine($"Стартовый склад: {startResources}");
        Console.WriteLine($"Прирост за день: {dailyIncome}");
        Console.WriteLine($"Итого на складе: {total}");
    }
}