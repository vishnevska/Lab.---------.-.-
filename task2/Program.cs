using System;

class Transaction
{
    public int Month { get; set; }
    public double Amount { get; set; }
}

class Program
{
    static void Main()
    {
        double[] monthlyTotals = new double[12];

        Transaction[] transactions = new Transaction[]
        {
            new Transaction { Month = 1, Amount = 1500.0 },
            new Transaction { Month = 3, Amount = 2300.5 },
            new Transaction { Month = 1, Amount = 500.0 },
            new Transaction { Month = 12, Amount = 4100.0 },
            new Transaction { Month = 3, Amount = 700.0 }
        };
        foreach (var tx in transactions)
        {
            int index = tx.Month - 1;
            monthlyTotals[index] += tx.Amount;
        }
        Console.WriteLine("=== Підсумки за місяцями ===");
        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine($"Місяць {i + 1,2}: {monthlyTotals[i],8:F2} грн");
        }
    }
}