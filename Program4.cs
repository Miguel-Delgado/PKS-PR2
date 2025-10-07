using System;

class NumberGuesser
{
    static void Main()
    {
        Console.WriteLine("Загадайте число от 0 до 63. Я попробую его угадать.");
        Console.WriteLine("Отвечайте '1' (да) или '0' (нет) на мои вопросы.");

        int low = 0;
        int high = 63;
        int questionsCount = 0;

        while (low <= high)
        {
            int mid = (low + high) / 2;
            questionsCount++;

            if (low == high)
            {
                Console.WriteLine($"Ваше число: {low}");
                break;
            }

            // Запрашиваем ответ с проверкой корректности ввода
            string answer;
            while (true)
            {
                Console.Write($"Ваше число больше {mid}? (1/0): ");
                answer = Console.ReadLine();

                if (answer == "1" || answer == "0")
                {
                    break; // Корректный ввод, выходим из цикла проверки
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите только '1' (да) или '0' (нет)!");
                }
            }

            if (answer == "1")
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }

        Console.WriteLine($"Я угадал число за {questionsCount} вопросов!");
    }
}