using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var beforeA = new Dictionary<string, List<string>>
        {
            ["Вступ"] = new List<string>(),
            ["Математика"] = new List<string>(),
            ["ООП"] = new List<string> { "Вступ" },
            ["Бази даних"] = new List<string> { "Вступ" },
            ["Алгоритми"] = new List<string> { "Вступ", "Математика" },
            ["Вебтехнології"] = new List<string> { "ООП", "Бази даних" },
            ["Курсовий"] = new List<string> { "Алгоритми", "Вебтехнології" }
        };

        var afterA = new Dictionary<string, List<string>>
        {
            ["Вступ"] = new List<string> { "ООП", "Бази даних", "Алгоритми" },
            ["Математика"] = new List<string> { "Алгоритми" },
            ["ООП"] = new List<string> { "Вебтехнології" },
            ["Бази даних"] = new List<string> { "Вебтехнології" },
            ["Алгоритми"] = new List<string> { "Курсовий" },
            ["Вебтехнології"] = new List<string> { "Курсовий" },
            ["Курсовий"] = new List<string>()
        };

        var beforeB = new Dictionary<string, List<string>>(beforeA);
        beforeB["Бази даних"] = new List<string> { "Вступ", "Вебтехнології" };

        var afterB = new Dictionary<string, List<string>>(afterA);
        afterB["Вебтехнології"] = new List<string> { "Курсовий", "Бази даних" };

        var orderA = TopoSort(beforeA, afterA);
        bool cycleA = HasCycle(beforeA, afterA);

        Console.WriteLine("=== НАБІР А ===");
        Console.WriteLine($"Кількість предметів: {orderA.Count}");
        Console.WriteLine($"Порядок: {string.Join(" -> ", orderA)}");
        Console.WriteLine($"hasCycle(А): {cycleA}");

        var orderB = TopoSort(beforeB, afterB);
        bool cycleB = HasCycle(beforeB, afterB);

        Console.WriteLine("\n=== НАБІР Б ===");
        Console.WriteLine($"Кількість предметів: {orderB.Count}");
        Console.WriteLine($"Порядок: {string.Join(" -> ", orderB)}");
        Console.WriteLine($"hasCycle(Б): {cycleB}");

        var missingB = new List<string>();
        foreach (var subject in beforeB.Keys)
        {
            if (!orderB.Contains(subject))
                missingB.Add(subject);
        }
        Console.WriteLine($"Не потрапили в Б: {string.Join(", ", missingB)}");
    }

    static List<string> TopoSort(Dictionary<string, List<string>> before, Dictionary<string, List<string>> after)
    {
        var incoming = new Dictionary<string, int>();
        foreach (var pair in before)
        {
            incoming[pair.Key] = pair.Value.Count;
        }

        var queue = new Queue<string>();
        foreach (var pair in incoming)
        {
            if (pair.Value == 0)
            {
                queue.Enqueue(pair.Key);
            }
        }

        var order = new List<string>();
        while (queue.Count > 0)
        {
            string v = queue.Dequeue();
            order.Add(v);

            foreach (var next in after[v])
            {
                incoming[next]--;
                if (incoming[next] == 0)
                {
                    queue.Enqueue(next);
                }
            }
        }

        return order;
    }

    static bool HasCycle(Dictionary<string, List<string>> before, Dictionary<string, List<string>> after)
    {
        var order = TopoSort(before, after);
        return order.Count != before.Count;
    }
}