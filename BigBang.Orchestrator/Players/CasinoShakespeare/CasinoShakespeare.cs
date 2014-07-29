using System;

public class SimpleRandomBot
{
    public static void Main(string[] args)
    {
        var s = new[] {"R", "P", "S", "L", "V"};

        if (args.Length == 0)
        {
            Console.WriteLine("V"); //always start with spock
            return;
        }

        char[] myPreviousPlays = args[0].ToCharArray();
        char[] oppPreviousPlays = args[1].ToCharArray();

        Random r = new Random();
        int next = r.Next(0, 5);

        Console.WriteLine(s[next]);
    }
}
