using System;
using System.Collections.Generic;

class Request
{
    public string Name { get; set; }
    public int Priority { get; set; } // 1 — критично, 2 — важливо, 3 — може почекати
    public int Seq { get; set; }      // порядковий номер надходження

    public Request(string name, int priority, int seq)
    {
        Name = name;
        Priority = priority;
        Seq = seq;
    }
}

class Program
{
    static void Main()
    {
        List<Request> incoming = new List<Request>
        {
            new Request("Оновити підпис у листі", 3, 1),
            new Request("Сервер не відповідає", 1, 2),
            new Request("Не друкує принтер", 2, 3),
            new Request("Не працює термінал оплати", 1, 4),
            new Request("Змінити шпалери", 3, 5)
        };

        Request late = new Request("Відвалився інтернет у відділі", 1, 6);

        Console.WriteLine("=== ЗАДАЧА 1: processAll ===");
        ProcessAll(incoming);

        Console.WriteLine("\n=== ЗАДАЧА 2: processStable ===");
        ProcessStable(incoming);

        Console.WriteLine("\n=== ЗАДАЧА 3: processWithLate ===");
        ProcessWithLate(incoming, late);
    }
    static void ProcessAll(List<Request> incoming)
    {
        PriorityQueue<Request, int> queue = new PriorityQueue<Request, int>();

        foreach (var req in incoming)
        {
            queue.Enqueue(req, req.Priority);
        }

        int step = 1;
        while (queue.Count > 0)
        {
            Request current = queue.Dequeue();
            Console.WriteLine($"{step}. {current.Name} (пріоритет: {current.Priority}, seq: {current.Seq})");
            step++;
        }
    }
    static void ProcessStable(List<Request> incoming)
    {
        PriorityQueue<Request, (int priority, int seq)> queue = new PriorityQueue<Request, (int, int)>();

        foreach (var req in incoming)
        {
            queue.Enqueue(req, (req.Priority, req.Seq));
        }

        int step = 1;
        while (queue.Count > 0)
        {
            Request current = queue.Dequeue();
            Console.WriteLine($"{step}. {current.Name} (пріоритет: {current.Priority}, seq: {current.Seq})");
            step++;
        }
    }

    static void ProcessWithLate(List<Request> incoming, Request late)
    {
        PriorityQueue<Request, (int priority, int seq)> queue = new PriorityQueue<Request, (int, int)>();

        foreach (var req in incoming)
        {
            queue.Enqueue(req, (req.Priority, req.Seq));
        }

        int step = 1;
        for (int i = 0; i < 2 && queue.Count > 0; i++)
        {
            Request current = queue.Dequeue();
            Console.WriteLine($"{step}. {current.Name} (пріоритет: {current.Priority}, seq: {current.Seq})");
            step++;
        }
        queue.Enqueue(late, (late.Priority, late.Seq));
        while (queue.Count > 0)
        {
            Request current = queue.Dequeue();
            Console.WriteLine($"{step}. {current.Name} (пріоритет: {current.Priority}, seq: {current.Seq})");
            step++;
        }
    }
}