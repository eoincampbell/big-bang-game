using System;
using System.Collections.Generic;
using System.Linq;

public class HuddleWolfHatesBigBangTheory
{

    public static readonly char[] s = new[] { 'R', 'P', 'S', 'L', 'V' };

    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine(pickRandom());
            return;
        }

        char[] myPlays = args[0].ToCharArray();
        char[] oppPlays = args[1].ToCharArray();

        char tryPredict = canPredictCounter(oppPlays);
        if (tryPredict != '^')
        {
            Console.WriteLine(tryPredict);
            return;
        }
        else
        {
            Console.WriteLine(pickRandom());
            return;
        }
    }


    public static char canPredictCounter(char[] history)
    {
        // don't predict if insufficient data
        if (history.Length < 10)
        {
            return '^';
        }

        // calculate probability of win for each choice
        Dictionary<char, double> dic = getBestProabability(history);

        // get item with highest probability of win
        List<char> maxVals = new List<char>();
        char maxVal = '^';
        double mostFreq = 0;
        foreach (var kvp in dic)
        {
            if (kvp.Value > mostFreq)
            {
                mostFreq = kvp.Value;
            }
        }
        foreach (var kvp in dic)
        {
            if (kvp.Value == mostFreq)
            {
                maxVals.Add(kvp.Key);
            }
        }

        // return error
        if (maxVals.Count == 0)
        {
            return maxVal;
        }

        // if distribution is not uniform, play best play
        if (maxVals.Count <= 3)
        {
            Random r = new Random(Environment.TickCount);
            return maxVals[r.Next(0, maxVals.Count)];
        }

        // if probability is close to uniform, use weighted dice roll
        if (maxVals.Count == 4)
        {
            return weightedRandom(dic);
        }

        // if probability is uniform, use random dice roll
        if (maxVals.Count >= 5)
        {
            return pickRandom();
        }

        // return error
        return '^';
    }

    public static Dictionary<char, double> getBestProabability(char[] history)
    {
        Dictionary<char, double> dic = new Dictionary<char, double>();
        foreach (char c in s)
        {
            dic.Add(c, 0);
        }
        foreach (char c in history)
        {
            if (dic.ContainsKey(c))
            {
                switch (c)
                {
                    case 'R':
                        dic['P'] += (1.0 / (double)history.Length);
                        dic['V'] += (1.0 / (double)history.Length);
                        break;
                    case 'P':
                        dic['S'] += (1.0 / (double)history.Length);
                        dic['L'] += (1.0 / (double)history.Length);
                        break;
                    case 'S':
                        dic['V'] += (1.0 / (double)history.Length);
                        dic['R'] += (1.0 / (double)history.Length);
                        break;
                    case 'L':
                        dic['R'] += (1.0 / (double)history.Length);
                        dic['S'] += (1.0 / (double)history.Length);
                        break;
                    case 'V':
                        dic['L'] += (1.0 / (double)history.Length);
                        dic['P'] += (1.0 / (double)history.Length);
                        break;
                    default:
                        break;

                }
            }
        }
        return dic;
    }

    public static char weightedRandom(Dictionary<char, double> dic)
    {
        Random r = new Random(Environment.TickCount);
        int next = r.Next(0, 100);
        int curVal = 0;
        foreach (var kvp in dic)
        {
            curVal += (int)(kvp.Value * 100);
            if (curVal > next)
            {
                return kvp.Key;
            }
        }
        return '^';
    }

    public static char pickRandom()
    {
        Random r = new Random(Environment.TickCount);
        int next = r.Next(0, 5);
        return s[next];
    }
}