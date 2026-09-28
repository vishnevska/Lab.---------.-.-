using System;

class Program
{
    static void Main()
    {
        int n = 5;
        string[] buffer = new string[n];
        int writeIndex = 0;
        int totalEvents = 0;

        string[] eventsStream = new string[]
        {
            "Event 1: Booting",
            "Event 2: Connecting to DB",
            "Event 3: Request received",
            "Event 4: Auth success",
            "Event 5: Data fetched",
            "Event 6: Cache updated",
            "Event 7: Response sent" 
        };

        foreach (var evt in eventsStream)
        {
            buffer[writeIndex] = evt;
            writeIndex = (writeIndex + 1) % n;
            totalEvents++;
        }

        Console.WriteLine("=== Останні 5 подій ===");
        int count = Math.Min(totalEvents, n);
        int startIndex = totalEvents >= n ? writeIndex : 0;

        for (int i = 0; i < count; i++)
        {
            int currIndex = (startIndex + i) % n;
            Console.WriteLine($"[{i + 1}] {buffer[currIndex]}");
        }
    }
}