using System;
using System.Text;

class Node
{
    public int Value { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

class Program
{
    static int compareCount = 0;

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Node root = null;
        int[] values = { 42, 18, 65, 9, 27, 55, 90, 33 };

        foreach (int val in values)
        {
            root = Insert(root, val);
        }

        Console.WriteLine($"Вузлів у дереві після 8 вставок: {CountNodes(root)}");

        Console.Write("Результат симетричного обходу (inorder): ");
        Inorder(root);
        Console.WriteLine();

        compareCount = 0;
        bool found55 = Search(root, 55);
        Console.WriteLine($"search(root, 55): {(found55 ? "так" : "ні")} (порівнянь: {compareCount})");

        compareCount = 0;
        bool found30 = Search(root, 30);
        Console.WriteLine($"search(root, 30): {(found30 ? "так" : "ні")} (порівнянь: {compareCount})");

        Console.WriteLine($"Глибина найдовшої гілки: {MaxDepth(root)}");

        root = Insert(root, 27);
        Console.WriteLine($"Вузлів після повторної вставки 27: {CountNodes(root)}");
    }
    static Node Insert(Node node, int key)
    {
        if (node == null)
        {
            return new Node(key);
        }

        if (key < node.Value)
        {
            node.Left = Insert(node.Left, key);
        }
        else if (key > node.Value)
        {
            node.Right = Insert(node.Right, key);
        }

        return node;
    }
    static bool Search(Node node, int key)
    {
        if (node == null)
        {
            return false;
        }

        compareCount++; 
        if (key == node.Value)
        {
            return true;
        }
        else if (key < node.Value)
        {
            return Search(node.Left, key);
        }
        else
        {
            return Search(node.Right, key);
        }
    }
    static void Inorder(Node node)
    {
        if (node == null)
        {
            return;
        }

        Inorder(node.Left);
        Console.Write($"{node.Value} ");
        Inorder(node.Right);
    }
    static int CountNodes(Node node)
    {
        if (node == null) return 0;
        return 1 + CountNodes(node.Left) + CountNodes(node.Right);
    }

    static int MaxDepth(Node node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(MaxDepth(node.Left), MaxDepth(node.Right));
    }
}