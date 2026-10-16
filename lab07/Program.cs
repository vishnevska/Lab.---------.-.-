using System;
using System.Collections.Generic;
using System.Linq;

class Request
{
    public string Name { get; set; }
    public int Minutes { get; set; }
    public Request(string name, int minutes)
    {
        Name = name;
        Minutes = minutes;
    }
}
class Program
{
    static void Main()
    {
        string[] lines = new string[]
        {
            "",
            "a + b",
            "{[a] + (b)}",
            "([)]",
            "a)",
            "(a[b]",
            "f(x[1]) * {y + (z)}"
        };
        Console.WriteLine(" ЗАДАЧА 1: Перевірка дужок ");
        foreach (var line in lines)
        {
            string displayLine = string.IsNullOrEmpty(line) ? "(порожній рядок)" : line;
            Console.WriteLine($"{displayLine} -> {(IsBalanced(line) ? "так" : "ні")}");
        }

        Console.WriteLine("\n ЗАДАЧА 2: Обробка заявок");
        List<Request> requests = new List<Request>
        {
            new Request("Довідка про навчання", 4),
            new Request("Академічна відпустка", 7),
            new Request("Копія наказу", 2),
            new Request("Виписка з оцінками", 6),
            new Request("Перескладання", 9),
            new Request("Студентський квиток", 3)
        };

        int totalTime = ProcessQueue(requests);
        Console.WriteLine($"Загальний час: {totalTime} хв");
    }
    static bool IsBalanced(string line)
    {
        if (string.IsNullOrEmpty(line)) return true;
        Stack<char> stack = new Stack<char>();
        foreach (char c in line)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.Count == 0) return false;
                char top = stack.Pop();
                if ((c == ')' && top != '(') ||
                    (c == ']' && top != '[') ||
                    (c == '}' && top != '{'))
                {
                    return false;
                }
            }
        }
        return stack.Count == 0;
    }
    static int ProcessQueue(IEnumerable<Request> requestsList)
    {
        if (requestsList == null || !requestsList.Any())
        {
            Console.WriteLine("Черга порожня, обробляти нічого.");
            return 0;
        }
        Queue<Request> queue = new Queue<Request>(requestsList);
        int currentTime = 0;
        int order = 1;
        int count = 0;
        while (queue.Count > 0)
        {
            Request current = queue.Dequeue();
            int startTime = currentTime;
            int endTime = startTime + current.Minutes;
            Console.WriteLine($"{order}. {current.Name} з {startTime} до {endTime} хв");
            currentTime = endTime;
            order++;
            count++;
        }
        Console.WriteLine($"Оброблено заявок: {count}, загальний час: {currentTime} хв");
        return currentTime;
    }
}