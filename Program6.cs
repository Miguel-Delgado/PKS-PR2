using System;

class Program
{
    static void Main()
    {
        // Ввод начальных данных
        Console.Write("Введите количество бактерий (N): ");
        int initialBacteria = int.Parse(Console.ReadLine());
        
        Console.Write("Введите количество капель антибиотика (X): ");
        int antibioticDrops = int.Parse(Console.ReadLine());

        // Инициализация переменных
        int currentBacteria = initialBacteria;
        int hour = 0;
        int antibioticEffectiveness = 10; // Начальная эффективность одной капли
        
        Console.WriteLine("\nДинамика изменения количества бактерий:");

        // Основной цикл моделирования
        while (antibioticEffectiveness > 0 && currentBacteria > 0)
        {
            hour++;
            
            // Фаза размножения бактерий
            currentBacteria *= 2;
            
            // Фаза действия антибиотика
            int bacteriaKilled = antibioticDrops * antibioticEffectiveness;
            currentBacteria = Math.Max(0, currentBacteria - bacteriaKilled);
            
            // Вывод текущего состояния
            Console.WriteLine($"Час {hour}: Бактерий = {currentBacteria}, " +
                            $"Убито антибиотиком = {bacteriaKilled}, " +
                            $"Эффективность капли = {antibioticEffectiveness}");
            
            // Уменьшение эффективности антибиотика
            antibioticEffectiveness--;
        }

        // Вывод итоговых результатов
        Console.WriteLine($"\nПроцесс завершен через {hour} часов");
        Console.WriteLine($"Конечное количество бактерий: {currentBacteria}");
        
        // Дополнительный анализ результата
        if (currentBacteria == 0)
        {
            Console.WriteLine("Антибиотик победил все бактерии!");
        }
        else if (antibioticEffectiveness == 0)
        {
            Console.WriteLine("Антибиотик потерял эффективность, бактерии продолжают размножаться!");
        }
    }
}