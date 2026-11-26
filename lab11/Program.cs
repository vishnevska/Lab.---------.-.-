using System;
using System.Collections.Generic;
using System.Text;

class Node
{
    public string Title { get; set; }
    public List<Node> Children { get; set; }

    public Node(string title, List<Node> children)
    {
        Title = title;
        Children = children ?? new List<Node>();
    }
}

class Program
{
    static int callCount = 0;

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Node menu = new Node("Головне меню", new List<Node>
        {
            new Node("Файл", new List<Node>
            {
                new Node("Новий", new List<Node>()),
                new Node("Зберегти", new List<Node>())
            }),
            new Node("Правка", new List<Node>
            {
                new Node("Копіювати", new List<Node>())
            }),
            new Node("Вигляд", new List<Node>
            {
                new Node("Масштаб", new List<Node>
                {
                    new Node("Збільшити", new List<Node>()),
                    new Node("Зменшити", new List<Node>())
                })
            }),
            new Node("Довідка", new List<Node>())
        });

        Console.WriteLine("=== TODO 1: printMenu ===");
        PrintMenu(menu, 0);

        Console.WriteLine("\n=== TODO 2: countItems ===");
        callCount = 0;
        int totalItems = CountItems(menu);
        Console.WriteLine($"Кількість пунктів меню: {totalItems}");
        Console.WriteLine($"Кількість викликів countItems: {callCount}");

        Console.WriteLine("\n=== TODO 3: find ===");
        Console.WriteLine($"find(menu, \"Зберегти\"): {(Find(menu, "Зберегти") ? "так" : "ні")}");
        Console.WriteLine($"find(menu, \"Масштаб\"): {(Find(menu, "Масштаб") ? "так" : "ні")}");
        Console.WriteLine($"find(menu, \"Довідка\"): {(Find(menu, "Довідка") ? "так" : "ні")}");
        Console.WriteLine($"find(menu, \"Експорт\"): {(Find(menu, "Експорт") ? "так" : "ні")}");
    }
    static void PrintMenu(Node node, int level)
    {
        if (node == null) return;

        string indent = new string(' ', level * 2);
        Console.WriteLine($"{indent}{node.Title}");

        foreach (var child in node.Children)
        {
            PrintMenu(child, level + 1);
        }
    }
    static int CountItems(Node node)
    {
        callCount++;
        if (node == null) return 0;

        int total = 1;

        foreach (var child in node.Children)
        {
            total += CountItems(child);
        }

        return total;
    }
    static bool Find(Node node, string name)
    {
        if (node == null) return false;
        if (node.Title == name) return true; 

        foreach (var child in node.Children)
        {
            if (Find(child, name))
            {
                return true;
            }
        }

        return false;
    }
}