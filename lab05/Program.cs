using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Surname { get; set; }
    public string Group { get; set; }
    public double Grade { get; set; } // середній бал
    public int Year { get; set; }     // рік вступу

    public Student(string surname, string group, double grade, int year)
    {
        Surname = surname;
        Group = group;
        Grade = grade;
        Year = year;
    }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student("Ткаченко", "ІПЗ-3/1", 85, 2024),
            new Student("Бондар", "ІПЗ-3/2", 92, 2023),
            new Student("Іваненко", "ІПЗ-3/1", 85, 2024),
            new Student("Коваль", "ІПЗ-3/2", 78, 2024),
            new Student("Сидоренко", "ІПЗ-3/1", 85, 2023),
            new Student("Мельник", "ІПЗ-3/2", 92, 2024),
            new Student("Гриценко", "ІПЗ-3/1", 78, 2023),
            new Student("Дяченко", "ІПЗ-3/2", 85, 2023),
        };

        var sortedByName = students.OrderBy(s => s.Surname).ToList();
        PrintAll("-1. За прізвищем, за абеткою", sortedByName);

        var sortedByGrade = students.OrderByDescending(s => s.Grade).ToList();
        PrintAll("-2. За балом (від вищого до нижчого)", sortedByGrade);

        var sortedByGroupAndGrade = students
            .OrderBy(s => s.Group)
            .ThenByDescending(s => s.Grade)
            .ToList();
        PrintAll("-3. За групою (головний), а всередині — за балом від вищого", sortedByGroupAndGrade);

        var sortedByGradeThenName = students
            .OrderByDescending(s => s.Grade)
            .ThenBy(s => s.Surname)
            .ToList();
        PrintAll("- 2 (модифіковане). За балом, а однакові — за абеткою", sortedByGradeThenName);
    }

    static void PrintAll(string title, List<Student> items)
    {
        Console.WriteLine(title);
        foreach (var s in items)
        {
            Console.WriteLine($"{s.Surname,-10} | {s.Group,-7} | {s.Grade,2} | {s.Year}");
        }
        Console.WriteLine();
    }
}