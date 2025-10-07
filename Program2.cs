using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите шестизначный номер билета: ");
        int ticketNumber;
        
        // Проверяем корректность ввода
        if (!int.TryParse(Console.ReadLine(), out ticketNumber) || ticketNumber < 100000 || ticketNumber > 999999)
        {
            Console.WriteLine("Ошибка: введите корректный шестизначный номер");
            return;
        }

        // Извлекаем цифры из номера билета
        int digit1 = ticketNumber / 100000;
        int digit2 = (ticketNumber / 10000) % 10;
        int digit3 = (ticketNumber / 1000) % 10;
        int digit4 = (ticketNumber / 100) % 10;
        int digit5 = (ticketNumber / 10) % 10;
        int digit6 = ticketNumber % 10;

        // Вычисляем суммы
        int sumFirst = digit1 + digit2 + digit3;
        int sumLast = digit4 + digit5 + digit6;

        // Выводим результат
        Console.WriteLine(sumFirst == sumLast ? "Билет счастливый!" : "Билет обычный.");
    }
}