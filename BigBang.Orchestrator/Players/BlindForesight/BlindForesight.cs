using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SO.BlindForesight
{

    /// <summary>
    /// Blind Foresight
    /// </summary>
    class Program
    {
        public enum Hand
        {
            Rock,
            Paper,
            Scissors,
            Vulcan,
            Lizard
        }

        public enum Outcome
        {
            Win=1,
            Tie=0,
            Loss=-1,
        }

        static Random rnd=new Random();
        static Dictionary<char, Hand> interpreter;
        static Program()
        {
            interpreter=new Dictionary<char, Hand>();
            foreach(var item in Enum.GetValues(typeof(Hand)))
            {
                Hand hand=(Hand)item;
                char key=hand.ToString()[0];
                interpreter[key]=hand;
            }
        }

        public struct Game
        {
            public Game(Hand mine, Hand other)
            {
                this.mine=mine;
                this.other=other;
                this.result=mine==other?Outcome.Tie:
                    ((int)mine+2)%5==(int)other||((int)mine+4)%5==(int)other?Outcome.Win:
                        Outcome.Loss;
            }
            public Game(char mine, char other) : this(interpreter[mine], interpreter[other]) { }
            public readonly Hand mine, other;
            public readonly Outcome result;
        }

        static void Main(string[] args)
        {
            if(args.Length==0) { Return(rnd.Next(0, 5)); }
            else if(args.Length==1) { throw new ArgumentException(); }
            else
            {
                int n=args[0].Length;
                Game last=new Game(args[0][n-1], args[1][n-1]);
                if(last.result==Outcome.Loss)
                {
                    Return((int)last.other+rnd.Next(0, 2)*2-1);
                }
                else
                {
                    Return((int)last.mine+rnd.Next(0, 2)*2-1);
                }
            }
        }
        static void Return(int hand)
        {
            if(hand<0) { hand+=5*(1-hand/5); }
            Console.WriteLine(((Hand)(hand%5)).ToString()[0]);
        }
    }
}