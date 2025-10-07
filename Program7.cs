using System;

class Program
{
    static void Main()
    {
        // Ввод данных
        Console.Write("Введите количество модулей (n): ");
        int n = int.Parse(Console.ReadLine());

        Console.Write("Введите размеры модуля (a b): ");
        string[] moduleSizes = Console.ReadLine().Split();
        int a = int.Parse(moduleSizes[0]);
        int b = int.Parse(moduleSizes[1]);

        Console.Write("Введите размеры поля (w h): ");
        string[] fieldSizes = Console.ReadLine().Split();
        int w = int.Parse(fieldSizes[0]);
        int h = int.Parse(fieldSizes[1]);

        // Вычисление максимальной толщины защиты
        int maxD = FindMaxProtectionThickness(n, a, b, w, h);

        // Вывод результата
        if (maxD == -1)
            Console.WriteLine("Размещение модулей невозможно даже без защиты");
        else
            Console.WriteLine($"Максимальная толщина защиты: {maxD}");
    }


    /// Находит максимальную толщину защиты для размещения модулей на поле
    static int FindMaxProtectionThickness(int n, int a, int b, int w, int h)
    {
        // Проверка возможности размещения без защиты
        if (!CanPlaceModules(n, a, b, w, h, 0))
            return -1;

        // Определение верхней границы для бинарного поиска
        int maxPossibleD = Math.Min(w, h) / 2;
        int left = 0;
        int right = maxPossibleD;
        int result = 0;

        // Бинарный поиск максимальной толщины защиты
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            
            if (CanPlaceModules(n, a, b, w, h, mid))
            {
                result = mid;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return result;
    }


    /// Проверяет возможность размещения модулей с заданной толщиной защиты
    static bool CanPlaceModules(int n, int a, int b, int w, int h, int protectionThickness)
    {
        // Вычисляем размеры модуля с защитой
        int protectedWidth = a + 2 * protectionThickness;
        int protectedHeight = b + 2 * protectionThickness;

        // Проверяем обе возможные ориентации модуля
        bool orientation1 = CanArrangeModules(n, protectedWidth, protectedHeight, w, h);
        bool orientation2 = CanArrangeModules(n, protectedHeight, protectedWidth, w, h);

        return orientation1 || orientation2;
    }


    /// Проверяет возможность размещения модулей в заданной ориентации
    static bool CanArrangeModules(int n, int moduleWidth, int moduleHeight, int fieldWidth, int fieldHeight)
    {
        // Проверяем, помещается ли модуль в поле
        if (moduleWidth > fieldWidth || moduleHeight > fieldHeight)
            return false;

        // Вычисляем максимальное количество модулей по горизонтали и вертикали
        int horizontalModules = fieldWidth / moduleWidth;
        int verticalModules = fieldHeight / moduleHeight;

        // Проверяем, достаточно ли места для всех модулей
        return (long)horizontalModules * verticalModules >= n;
    }
}