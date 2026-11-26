using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var graph = new Dictionary<string, List<(string, int)>>();
        graph["Склад"] = new List<(string, int)> { ("Школа", 7), ("Ринок", 2), ("Лікарня", 12) };
        graph["Школа"] = new List<(string, int)> { ("Склад", 7), ("Ринок", 3), ("Бібліотека", 4) };
        graph["Ринок"] = new List<(string, int)> { ("Склад", 2), ("Школа", 3), ("Лікарня", 9) };
        graph["Лікарня"] = new List<(string, int)> { ("Склад", 12), ("Ринок", 9), ("Вокзал", 6), ("Парк", 5) };
        graph["Бібліотека"] = new List<(string, int)> { ("Школа", 4), ("Вокзал", 1) };
        graph["Вокзал"] = new List<(string, int)> { ("Бібліотека", 1), ("Лікарня", 6), ("Парк", 3) };
        graph["Парк"] = new List<(string, int)> { ("Вокзал", 3), ("Лікарня", 5) };
        graph["Хутір"] = new List<(string, int)>();

        var (dist, prev, taken, processed) = Dijkstra(graph, "Склад");
        Console.WriteLine($"Вийнято з черги: {taken}; оброблено вершин: {processed}");

        foreach (var target in new[] { "Парк", "Лікарня", "Хутір" })
        {
            var route = BuildRoute(prev, "Склад", target);
            if (route.Count == 0)
            {
                Console.WriteLine($"{target}: маршруту немає");
            }
            else
            {
                Console.WriteLine($"{target}: {string.Join(" → ", route)} ({dist[target]} хв)");
            }
        }
    }

    static (Dictionary<string, int> dist, Dictionary<string, string> prev, int taken, int processed) Dijkstra(
        Dictionary<string, List<(string, int)>> graph, string start)
    {
        var dist = new Dictionary<string, int>();
        var prev = new Dictionary<string, string>();
        var done = new HashSet<string>();
        int taken = 0;
        int processed = 0;

        foreach (var v in graph.Keys)
        {
            dist[v] = int.MaxValue;
        }
        dist[start] = 0;

        var queue = new PriorityQueue<string, int>();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            queue.TryDequeue(out string v, out int d);
            taken++;

            if (done.Contains(v))
                continue;
            done.Add(v);

            processed++;

            foreach (var (nb, w) in graph[v])
            {
                int candidate = dist[v] + w;
                if (candidate < dist[nb])
                {
                    dist[nb] = candidate;
                    prev[nb] = v;
                    queue.Enqueue(nb, candidate);
                }
            }
        }

        return (dist, prev, taken, processed);
    }

    static List<string> BuildRoute(Dictionary<string, string> prev, string start, string target)
    {
        var route = new List<string>();
        if (start != target && !prev.ContainsKey(target))
            return route;

        string current = target;
        while (current != null)
        {
            route.Add(current);
            if (current == start)
                break;
            prev.TryGetValue(current, out current);
        }

        route.Reverse();
        return route;
    }
}