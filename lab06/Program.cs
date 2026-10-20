using System; using System.Collections.Generic;
class Category { public string Name { get; set; } public int Products { get; set; } public List<Category> Children { get; set; }
public Category(string name, int products, List<Category> children)
{
    Name = name;
    Products = products;
    Children = children ?? new List<Category>();
}
}
class Program { static int callCount = 0;
static void Main()
{
    Category catalog = new Category("Каталог", 0, new List<Category>
    {
        new Category("Одяг", 0, new List<Category>
        {
            new Category("Верхній одяг", 0, new List<Category>
            {
                new Category("Куртки", 12, new List<Category>()),
                new Category("Пальта", 7, new List<Category>())
            }),
            new Category("Светри", 15, new List<Category>())
        }),
        new Category("Взуття", 4, new List<Category>
        {
            new Category("Кросівки", 23, new List<Category>()),
            new Category("Чоботи", 9, new List<Category>())
        }),
        new Category("Аксесуари", 0, new List<Category>
        {
            new Category("Сумки", 18, new List<Category>()),
            new Category("Ремені", 5, new List<Category>())
        })
    });

    Console.WriteLine(" 1: PrintTree");
    callCount = 0;
    PrintTree(catalog, 0);
    Console.WriteLine($"Кількість викликів PrintTree: {callCount}\n");

    Console.WriteLine(" CountProducts");
    callCount = 0;
    int totalProducts = CountProducts(catalog);
    Console.WriteLine($"Загальна кількість товарів: {totalProducts}");
    Console.WriteLine($"Кількість викликів CountProducts: {callCount}\n");

    Console.WriteLine("MaxDepth");
    callCount = 0;
    int depth = MaxDepth(catalog);
    Console.WriteLine($"Глибина найдовшої гілки: {depth}");
    Console.WriteLine($"Кількість викликів MaxDepth: {callCount}\n");
}
static void PrintTree(Category node, int level)
{
    callCount++;
    if (node == null) return;

    string indent = new string(' ', level * 2);
    Console.WriteLine($"{indent}{node.Name}");

    foreach (var child in node.Children)
    {
        PrintTree(child, level + 1);
    }
}
static int CountProducts(Category node)
{
    callCount++;
    if (node == null) return 0;

    int total = node.Products;

    foreach (var child in node.Children)
    {
        total += CountProducts(child);
    }

    return total;
}

static int MaxDepth(Category node)
{
    callCount++;
    if (node == null) return 0;
    if (node.Children.Count == 0) return 1;

    int maxChildDepth = 0;

    foreach (var child in node.Children)
    {
        int currentDepth = MaxDepth(child);
        if (currentDepth > maxChildDepth)
        {
            maxChildDepth = currentDepth;
        }
    }

    return 1 + maxChildDepth;
}
}