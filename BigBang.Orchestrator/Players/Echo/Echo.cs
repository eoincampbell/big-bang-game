using System;

namespace Echo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Random r = new Random();
                string[] options = new string[] { "R", "P", "S", "L", "V" };
                Console.WriteLine(options[r.Next(0, options.Length)]);
            }
            else if (args.Length == 2)
            {
                string opponentHistory = args[1];
                Console.WriteLine(opponentHistory[opponentHistory.Length - 1]);
            }
        }
    }
}