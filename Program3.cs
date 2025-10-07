using System;

class FractionReduction
{
    static void Main()
    {
        try
        {
            // Ввод данных в одной строке
            Console.Write("Введите числитель и знаменатель через пробел: ");
            string[] input = Console.ReadLine().Split();
            
            int m = int.Parse(input[0]);
            int n = int.Parse(input[1]);

            // Проверка на нулевой знаменатель
            if (n == 0)
            {
                Console.WriteLine("Ошибка: знаменатель не может быть нулем!");
                return;
            }

            // Нахождение НОД с учетом отрицательных чисел
            int gcd = GetGCD(m, n);
            
            // Сокращение дроби с корректировкой знака
            int numerator = m / gcd;
            int denominator = n / gcd;

            // Корректировка знака
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            // Вывод результата
            if (denominator == 1)
                Console.WriteLine($"Результат: {numerator}");
            else
                Console.WriteLine($"Результат: {numerator}/{denominator}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    // Рекурсивный метод для нахождения НОД
    static int GetGCD(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        
        if (b == 0) return a;
        return GetGCD(b, a % b);
    }
}