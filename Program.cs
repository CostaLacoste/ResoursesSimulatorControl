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

class Farm : Building
{
    public Farm(string name, int initialLevel = 1) : base(name, initialLevel)
    {
    }

    public override ResourceBundle Produce()
    {
        // Ферма производит еду (Food) пропорционально текущему уровню
        return new ResourceBundle(wood: 0, stone: 0, food: Level * 5);
    }

    public override ResourceBundle GetUpgradeCost()
    {
        // Стоимость улучшения фермы зависит от текущего уровня
        return new ResourceBundle(wood: Level * 5, stone: Level * 3, food: 0);
    }
}

class Mine : Building
{
    public Mine(string name, int initialLevel = 1) : base(name, initialLevel)
    {
    }

    public override ResourceBundle Produce()
    {
        // Шахта производит камень (Stone) пропорционально текущему уровню
        return new ResourceBundle(wood: 0, stone: Level * 4, food: 0);
    }

    public override ResourceBundle GetUpgradeCost()
    {
        // Стоимость улучшения шахты зависит от текущего уровня
        return new ResourceBundle(wood: Level * 6, stone: 0, food: Level * 2);
    }
}

class Program
{
    static void Main()
    {
        ResourceBundle startResources = new ResourceBundle(10, 5, 5);
        ResourceBundle dailyIncome = new ResourceBundle(3, 2, 4);
        ResourceBundle total = startResources.Add(dailyIncome);
        Farm farm = new Farm("Scout", 1);
        Mine mine = new Mine("Well", 1);

        Console.WriteLine($"Стартовый склад: {startResources}");
        Console.WriteLine($"Прирост за день: {dailyIncome}");
        Console.WriteLine($"Итого на складе: {total}");
    }
}