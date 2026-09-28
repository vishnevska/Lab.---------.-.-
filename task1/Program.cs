using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        List<string> log = new List<string>();
        log.Add("10:00:00 - Запуск системи");
        log.Add("10:02:15 - Вхід користувача");
        log.Add("10:05:40 - Помилка бази даних");
        log.Add("10:10:00 - Завершення сесії");
        Console.WriteLine("=== Журнал у зворотному порядку ===");
        for (int i = log.Count - 1; i >= 0; i--)
        {
            Console.WriteLine($"[{i}] {log[i]}");
        }
    }
}