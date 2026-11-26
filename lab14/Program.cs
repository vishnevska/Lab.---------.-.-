using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var graph = new Dictionary<string, List<string>>();
        graph["Центр"] = new List<string> { "Парк", "Ринок" };
        graph["Парк"] = new List<string> { "Центр", "Вокзал" };
        graph["Ринок"] = new List<string> { "Центр", "Вокзал", "Стадіон" };
        graph["Вокзал"] = new List<string> { "Парк", "Ринок", "Озеро" };
        graph["Стадіон"] = new List<string> { "Ринок" };
        graph["Озеро"] = new List<string> { "Вокзал" };
        graph["Гай"] = new List<string> { "Міст" };
        graph["Міст"] = new List<string> { "Гай" };

        var visitedFromCenter = VisitAll(graph, "Центр");
        Console.WriteLine($"visitAll(\"Центр\"): {visitedFromCenter.Count}");

        Console.WriteLine($"reachable(\"Центр\", \"Озеро\"): {Reachable(graph, "Центр", "Озеро")}");
        Console.WriteLine($"reachable(\"Центр\", \"Гай\"): {Reachable(graph, "Центр", "Гай")}");
        Console.WriteLine($"reachable(\"Гай\", \"Міст\"): {Reachable(graph, "Гай", "Міст")}");

        var pathToLake = FindPath(graph, "Центр", "Озеро");
        Console.WriteLine($"findPath(\"Центр\", \"Озеро\"): [{string.Join(" -> ", pathToLake)}] (ребер: {pathToLake.Count - 1})");

        var pathToStadium = FindPath(graph, "Центр", "Стадіон");
        Console.WriteLine($"findPath(\"Центр\", \"Стадіон\"): [{string.Join(" -> ", pathToStadium)}] (ребер: {pathToStadium.Count - 1})");

        var pathToBridge = FindPath(graph, "Центр", "Міст");
        Console.WriteLine($"findPath(\"Центр\", \"Міст\"): [{string.Join(" -> ", pathToBridge)}]");

        int unreachableCount = graph.Count - visitedFromCenter.Count;
        Console.WriteLine($"Недосяжних вершин з \"Центру\": {unreachableCount}");
    }

    static HashSet<string> VisitAll(Dictionary<string, List<string>> graph, string start)
    {
        var visited = new HashSet<string>();
        if (!graph.ContainsKey(start)) return visited;

        var queue = new Queue<string>();
        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();

            foreach (var neighbor in graph[current])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return visited;
    }

    static bool Reachable(Dictionary<string, List<string>> graph, string from, string to)
    {
        var visited = VisitAll(graph, from);
        return visited.Contains(to);
    }

    static List<string> FindPath(Dictionary<string, List<string>> graph, string from, string to)
    {
        if (!graph.ContainsKey(from) || !graph.ContainsKey(to))
            return new List<string>();

        var visited = new HashSet<string>();
        var parent = new Dictionary<string, string>();
        var queue = new Queue<string>();

        visited.Add(from);
        queue.Enqueue(from);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();

            if (current == to)
                break;

            foreach (var neighbor in graph[current])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    parent[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        if (!visited.Contains(to))
            return new List<string>();

        var path = new List<string>();
        string step = to;

        while (step != null)
        {
            path.Add(step);
            parent.TryGetValue(step, out step);
        }

        path.Reverse();
        return path;
    }
}