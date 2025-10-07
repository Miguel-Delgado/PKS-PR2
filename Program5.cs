using System;

class CoffeeMachine
{
    // Параметры напитков
    private const int AMERICANO_WATER = 300;
    private const int LATTE_WATER = 30;
    private const int LATTE_MILK = 270;
    
    private const int AMERICANO_PRICE = 150;
    private const int LATTE_PRICE = 170;
    
    // Текущие запасы
    private int water;
    private int milk;
    
    // Статистика
    private int americanoCount;
    private int latteCount;
    private int totalIncome;
    
    public void Start()
    {
        // Инициализация статистики
        americanoCount = 0;
        latteCount = 0;
        totalIncome = 0;
        
        // Запрос начальных запасов
        Console.WriteLine("=== КОФЕЙНЫЙ АППАРАТ ===");
        Console.Write("Введите количество воды (мл): ");
        water = int.Parse(Console.ReadLine());
        
        Console.Write("Введите количество молока (мл): ");
        milk = int.Parse(Console.ReadLine());
        
        Console.WriteLine("\nАппарат готов к работе!");
        
        // Основной цикл обслуживания
        while (CanMakeAnyDrink())
        {
            ServeCustomer();
        }
        
        // Вывод отчета когда ингредиенты закончились
        PrintReport();
    }
    
    private bool CanMakeAnyDrink()
    {
        // Проверяем, можно ли приготовить хотя бы один напиток
        return (water >= AMERICANO_WATER) || (water >= LATTE_WATER && milk >= LATTE_MILK);
    }
    
    private bool CanMakeAmericano()
    {
        return water >= AMERICANO_WATER;
    }
    
    private bool CanMakeLatte()
    {
        return water >= LATTE_WATER && milk >= LATTE_MILK;
    }
    
    private void ServeCustomer()
    {
        Console.WriteLine("\n=== ОБСЛУЖИВАНИЕ ПОСЕТИТЕЛЯ ===");
        Console.WriteLine("Доступные напитки:");
        
        if (CanMakeAmericano())
            Console.WriteLine("1 - Американо (150 руб)");
        else
            Console.WriteLine("1 - Американо - недоступно (не хватает воды)");
            
        if (CanMakeLatte())
            Console.WriteLine("2 - Латте (170 руб)");
        else
            Console.WriteLine("2 - Латте - недоступно (не хватает ингредиентов)");
        
        Console.Write("Выберите напиток (1 или 2): ");
        string choice = Console.ReadLine();
        
        switch (choice)
        {
            case "1":
                MakeAmericano();
                break;
            case "2":
                MakeLatte();
                break;
            default:
                Console.WriteLine("Неверный выбор. Пожалуйста, выберите 1 или 2.");
                break;
        }
    }
    
    private void MakeAmericano()
    {
        if (CanMakeAmericano())
        {
            water -= AMERICANO_WATER;
            americanoCount++;
            totalIncome += AMERICANO_PRICE;
            Console.WriteLine("Ваш напиток готов");
        }
        else
        {
            Console.WriteLine("Не хватает воды");
        }
    }
    
    private void MakeLatte()
    {
        if (CanMakeLatte())
        {
            water -= LATTE_WATER;
            milk -= LATTE_MILK;
            latteCount++;
            totalIncome += LATTE_PRICE;
            Console.WriteLine("Ваш напиток готов");
        }
        else
        {
            if (water < LATTE_WATER)
                Console.WriteLine("Не хватает воды");
            else if (milk < LATTE_MILK)
                Console.WriteLine("Не хватает молока");
        }
    }
    
    private void PrintReport()
    {
        Console.WriteLine("\n=== ОТЧЕТ ===");
        Console.WriteLine("Ингредиенты подошли к концу");
        Console.WriteLine($"Остаток воды: {water} мл");
        Console.WriteLine($"Остаток молока: {milk} мл");
        Console.WriteLine($"Приготовлено чашек американо: {americanoCount}");
        Console.WriteLine($"Приготовлено чашек латте: {latteCount}");
        Console.WriteLine($"Итоговый заработок: {totalIncome} руб.");
        
        Console.WriteLine("\nПрограмма завершена. Нажмите любую клавишу...");
        Console.ReadKey();
    }
    
    static void Main(string[] args)
    {
        CoffeeMachine machine = new CoffeeMachine();
        machine.Start();
    }
}