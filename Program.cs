using System;

class Program
{
    // Статический массив для кэширования факториалов
    private static double[] _factorialCache = new double[100];

    static Program()
    {
        // Предварительное вычисление факториалов при инициализации
        PrecomputeFactorials();
    }

    // Предварительное вычисление факториалов
    static void PrecomputeFactorials()
    {
        _factorialCache[0] = 1;
        for (int i = 1; i < _factorialCache.Length; i++)
        {
            _factorialCache[i] = _factorialCache[i - 1] * i;
        }
    }

    // Оптимизированная функция получения факториала
    static double GetCachedFactorial(int n)
    {
        if (n < _factorialCache.Length)
            return _factorialCache[n];

        // Для больших значений вычисляем на лету
        double result = _factorialCache[_factorialCache.Length - 1];
        for (int i = _factorialCache.Length; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    // Функция для вычисления n-го члена ряда Маклорена для e^x
    static double CalculateNthTerm(double x, int n)
    {
        return Math.Pow(x, n) / GetCachedFactorial(n);
    }

    // Оптимизированная функция вычисления суммы ряда
    static double CalculateSeriesSum(double x, double precision, out int iterations)
    {
        double sum = 0;
        double term = 1;
        iterations = 0;

        while (Math.Abs(term) > precision)
        {
            sum += term;
            iterations++;
            term = CalculateNthTerm(x, iterations);

            // Защита от бесконечного цикла
            if (iterations > 1000) break;
        }

        return sum;
    }

    // Функция для проверки корректности ввода
    static bool TryGetInput(string prompt, out double result)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();

        // Заменяем точку на запятую для корректного парсинга в русской локали
        if (input != null)
        {
            input = input.Replace('.', ',');
        }

        if (double.TryParse(input, out result))
            return true;

        Console.WriteLine("Ошибка! Введите корректное число.");
        return false;
    }

    static void Main()
    {
        Console.WriteLine("Вычисление функции e^x через ряд Маклорена");
        Console.WriteLine("==========================================");

        // Ввод значения x
        double x;
        while (!TryGetInput("Введите значение x: ", out x))
        {
            Console.WriteLine("Ошибка! Введите корректное число.");
        }

        // Ввод точности
        double precision;
        while (true)
        {
            if (!TryGetInput("Введите точность вычисления (e < 0.01): ", out precision))
            {
                Console.WriteLine("Ошибка! Введите корректное число.");
                continue;
            }

            if (precision >= 0.01)
            {
                Console.WriteLine("Ошибка! Точность должна быть меньше 0.01");
                continue;
            }

            if (precision <= 0)
            {
                Console.WriteLine("Ошибка! Точность должна быть положительным числом");
                continue;
            }

            break;
        }

        // Вычисление суммы ряда
        int iterations;
        double result = CalculateSeriesSum(x, precision, out iterations);

        Console.WriteLine($"\nРезультаты вычислений:");
        Console.WriteLine($"Значение e^{x} с точностью {precision}: {result:F10}");
        Console.WriteLine($"Стандартная Math.Exp({x}): {Math.Exp(x):F10}");
        Console.WriteLine($"Разница: {Math.Abs(Math.Exp(x) - result):E}");
        Console.WriteLine($"Количество итераций: {iterations}");

        // Вычисление n-го члена ряда
        Console.WriteLine($"\nАнализ членов ряда:");
        Console.Write("Введите номер члена ряда для анализа (n): ");
        if (int.TryParse(Console.ReadLine(), out int n) && n >= 0)
        {
            double nthTerm = CalculateNthTerm(x, n);
            Console.WriteLine($"Значение {n}-го члена ряда: {nthTerm:E}");

            // Показ нескольких первых членов
            Console.WriteLine($"\nПервые 5 членов ряда:");
            for (int i = 0; i < Math.Min(5, n + 3); i++)
            {
                double term = CalculateNthTerm(x, i);
                Console.WriteLine($"  n={i}: {term:E} {(term > precision ? "✓" : "✗")}");
            }
        }
        else
        {
            Console.WriteLine("Некорректный номер члена ряда!");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}