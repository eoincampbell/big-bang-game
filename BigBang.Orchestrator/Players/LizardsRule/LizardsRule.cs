using System;
public class LizardsRule
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("L");
            return;
        }
        char[] oppPreviousPlays = args[1].ToCharArray();
        var oppLen = oppPreviousPlays.Length;
        if (oppPreviousPlays.Length > 2
            && oppPreviousPlays[oppLen - 1] == 'R'
            && oppPreviousPlays[oppLen - 2] == 'R'
            && oppPreviousPlays[oppLen - 3] == 'R')
        {
            //It's an avalance, someone call Spock
            Console.WriteLine("V");
            return;
        }

        if (oppPreviousPlays.Length > 2
                && oppPreviousPlays[oppLen - 1] == 'S'
                && oppPreviousPlays[oppLen - 2] == 'S'
                && oppPreviousPlays[oppLen - 3] == 'S')
        {
            //Scissors, Drop your tail and pick up a rock
            Console.WriteLine("R");
            return;
        }

        //Unleash the Fury Godzilla
        Console.WriteLine("L");
    }
}