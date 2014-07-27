using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
namespace BigBang.Orchestrator
{
    using System.Diagnostics;
    using System.Security.Cryptography.X509Certificates;

    public class Program
    {
        public static string AppDirectory
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
        }

        public static string PlayerDirectory
        {
            get { return System.IO.Path.GetDirectoryName(AppDirectory) + "\\Players"; }
        }

        static void Main(string[] args)
        {
            List<Player> players = new List<Player>(){
                    //.NET
                    new Player { Author = "EoinC", Name = "SimpleRandomBot", BotExecutable = @"SimpleRandomBot\SimpleRandomBot.exe", RequiresCompile = true},
                    new Player { Author = "HuddleWolf", Name = "HuddleWolfHatesBigBangTheory", BotExecutable = @"HuddleWolfHatesBigBangTheory\HuddleWolfHatesBigBangTheory.exe", RequiresCompile = true},
                    new Player { Author = "ProgramFOX", Name = "Echo", BotExecutable = @"Echo\Echo.exe" , RequiresCompile = true},
                    new Player { Author = "Mikey Mouse", Name = "LizardsRule", BotExecutable = @"LizardsRule\LizardsRule.exe" , RequiresCompile = true},
                   
                    //JAVA
                    new Player { Author = "Stranjyr", Name = "ToddlerProof", JavaArgs = "ToddlerProof", BotExecutable = @"ToddlerProof", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe" },
                    new Player { Author = "kaine", Name = "BoringBot", JavaArgs = "BoringBot", BotExecutable = @"BoringBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe"},
                    new Player { Author = "Stretch Maniac", Name = "SmartBot", JavaArgs = "SmartBot", BotExecutable = @"SmartBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe" },

                    //Ruby
                    new Player { Author = "William Barbosa", Name = "StarWarsFan", BotExecutable = @"StarWarsFan\StarWarsFan.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Martin Büttner", Name = "ConservativeBot", BotExecutable = @"ConservativeBot\ConservativeBot.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},
                    new Player { Author = "Martin Büttner", Name = "SlowLizard", BotExecutable = @"SlowLizard\SlowLizard.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Martin Büttner", Name = "FairBot", BotExecutable = @"FairBot\FairBot.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Martin Büttner", Name = "Vulcan", BotExecutable = @"Vulcan\Vulcan.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Thaylon", Name = "NitPicker", BotExecutable = @"NitPicker\NitPicker.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Martin Büttner", Name = "MarkovBot", BotExecutable = @"MarkovBot\MarkovBot.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
//// BROKEN         new Player { Author = "histocrat", Name = "Analogizer", BotExecutable = @"Analogizer\Analogizer.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
//// BROKEN         new Player { Author = "Rory O'Kane", Name = "RandomlyWeighted", BotExecutable = @"RandomlyWeighted\RandomlyWeighted.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "histocrat", Name = "WereVulcan", BotExecutable = @"WereVulcan\WereVulcan.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    
                    //PYTHON 3
                    new Player { Author = "Timmy", Name = "DynamicBot", BotExecutable = @"DynamicBot\DynamicBot.py", PrefixCommand = @"C:\Python34\python.exe"},                    
                    new Player { Author = "Kyle Kanos", Name = "ViolentBot", BotExecutable = @"ViolentBot\ViolentBot.py", PrefixCommand = @"C:\Python34\python.exe"},                    
                    new Player { Author = "Kyle Kanos", Name = "LexicographicBot", BotExecutable = @"LexicographicBot\LexicographicBot.py", PrefixCommand = @"C:\Python34\python.exe"},                    
                    new Player { Author = "Trimsty", Name = "Herpetologist", BotExecutable = @"Herpetologist\Herpetologist.py", PrefixCommand = @"C:\Python34\python.exe"},                    
                        
                    //PYTHON 2
                    new Player { Author = "bitpwner", Name = "AlgorithmBot", BotExecutable = @"AlgorithmBot\AlgorithmBot.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "undergroundmonorail", Name = "TheGambler", BotExecutable = @"TheGambler\TheGambler.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "Emil", Name = "Pony", BotExecutable = @"Pony\Pony.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "Claudiu", Name = "SuperMarkov", BotExecutable = @"SuperMarkov\SuperMarkov.py", PrefixCommand = @"C:\Python27\python.exe"},                    

                    //LUA
                    new Player { Author = "William Barbosa", Name = "BarneyStinson", BotExecutable = @"BarneyStinson\BarneyStinson.lua", PrefixCommand = @"C:\Program Files (x86)\Lua\5.1\lua.exe"},

                    //PHP
                    new Player { Author = "ArcticanAudio", Name = "SpockOrRockBot", BotExecutable = @"SpockOrRockBot\SpockOrRockBot.php", PrefixCommand = @"C:\PHP5.15\php.exe"} ,                  

                    //PERL
                    new Player { Author = "PhiNotPi", Name = "BayesianBot", BotExecutable = @"BayesianBot\BayesianBot.perl", PrefixCommand = @"C:\strawberry\perl\bin\perl.exe"},                   
                    new Player { Author = "killmous", Name = "MAWBRBot", BotExecutable = @"MAWBRBot\MAWBRBot.perl", PrefixCommand = @"C:\strawberry\perl\bin\perl.exe"},                 

                    //HASKELL
                    new Player { Author = "DrJPepper", Name = "MonadBot", BotExecutable = @"MonadBot\MonadBot.exe" , RequiresCompile = true},

                    //SH (Cygwin)
//// BROKEN         new Player { Author = "mccannf", Name = "BashRocksBot", BotExecutable = @"BashRocksBot\BashRocksBot.sh", PrefixCommand = @"C:\Cygwin\bin\sh.exe"},                 

                };

           // BuildWhereRequired(players);



            var results = new List<Result>();

            List<Player> playersWhoPlayed = new List<Player>();

            foreach (Player p1 in players.Except(playersWhoPlayed))
            {
                playersWhoPlayed.Add(p1);
                foreach (Player p2 in players.Except(playersWhoPlayed))
                {
                    var result = Play(p1, p2);
                    results.Add(result);

                    Console.WriteLine("Result: {0} vs {1}: {2} - {3}", result.P1, result.P2, result.P1Score, result.P2Score);
                }
            }


            foreach (var r in results)
            {
                var p1 = players.First(p => p == r.P1);
                var p2 = players.First(p => p == r.P2);


                if (r.P1Score > r.P2Score)
                {
                    p1.LeagueScore++;
                }
                else if (r.P1Score < r.P2Score)
                {
                    p2.LeagueScore++;
                }

                p1.AvgDecisionTimes.Add(r.P1AvgTimeMs);
                p2.AvgDecisionTimes.Add(r.P2AvgTimeMs);

            }


            var resultGrid = players.OrderByDescending(p => p.LeagueScore);
            Console.WriteLine("| {0} | {1} | {2} | {3} |", "Author".PadRight(20), "Name".PadRight(20), "Score".PadRight(10),
                "Avg. Time".PadRight(15));
            Console.WriteLine("+----------------------+----------------------+------------+-----------------+");
            foreach (var rg in resultGrid)
            {
                Console.WriteLine("| {0} | {1} | {2:##0}          | {3:00.00} ms        |", rg.Author.PadRight(20), rg.Name.PadRight(20), rg.LeagueScore, rg.AvgDecisionTimes.Average());
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void BuildWhereRequired(List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.RequiresCompile)
                {
                    var dir = PlayerDirectory;

                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.WorkingDirectory = dir + "\\" + player.Name;
                    proc.StartInfo.FileName = dir + "\\" + player.Name + "\\Build.bat";
                    proc.Start();
                    proc.WaitForExit();
                }
            }

        }

        public static Result Play(Player p1, Player p2)
        {
            var dir = PlayerDirectory;

            var result = new Result() { P1 = p1, P2 = p2, P1Score = 0, P2Score = 0 };
            var player1ParamList = string.Empty;
            var player2ParamList = string.Empty;
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.WorkingDirectory = dir;
            
            var p1Times = new List<long>();
            var p2Times = new List<long>();

            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            for (var i = 0; i < 100; i++)
            {
                sw1.Reset();
                sw1.Start();
                var o1 = RunProcess(ref proc, player1ParamList, player2ParamList, p1, dir);
                sw1.Stop();
                p1Times.Add(sw1.ElapsedMilliseconds);

                System.Threading.Thread.Sleep(1);

                sw2.Reset();
                sw2.Start();
                var o2 = RunProcess(ref proc, player1ParamList, player2ParamList, p2, dir);
                sw2.Stop();
                p2Times.Add(sw2.ElapsedMilliseconds);

                player1ParamList += o1;
                player2ParamList += o2;

                var whoWon = GetWinner(o1, o2);

                if (whoWon == "P1")
                {
                    result.P1Score++;
                }
                else if (whoWon == "P2")
                {
                    result.P2Score++;
                }
            }

            result.P1AvgTimeMs = p1Times.Average();
            result.P2AvgTimeMs = p2Times.Average();

            return result;
        }

        public static string RunProcess(ref Process p, string player1ParamList, string player2ParamList, Player pl, string dir)
        {

            if (!string.IsNullOrEmpty(pl.PrefixCommand))
            {
                var exec = "\"" + dir + "\\" + pl.BotExecutable + "\"";
                p.StartInfo.FileName = pl.PrefixCommand;

                if (!string.IsNullOrEmpty(pl.JavaArgs))
                {
                    p.StartInfo.Arguments = string.IsNullOrEmpty(player1ParamList)
                        ? "-cp \"" + dir + "\\" + pl.JavaArgs + "\"" + " " + pl.BotExecutable
                        : "-cp \"" + dir + "\\" + pl.JavaArgs + "\"" + " " + pl.BotExecutable + " " + player1ParamList +
                          " " + player2ParamList;
                }
                else
                {

                    p.StartInfo.Arguments = string.IsNullOrEmpty(player1ParamList)
                        ? exec
                        : string.Join(" ", exec, player1ParamList, player2ParamList);

                }
            }
            else
            {
                var exec =  dir + "\\" + pl.BotExecutable;
                p.StartInfo.FileName = exec;
                p.StartInfo.Arguments = string.IsNullOrEmpty(player1ParamList)
                    ? string.Empty
                    : string.Join(" ", player1ParamList, player2ParamList);
            }

            p.Start();
            string output = p.StandardOutput.ReadToEnd().Trim();
            p.WaitForExit();

            return output;
        }

        public static string [] validAnswer  = { "R", "P", "S", "L", "V" };

        public static List<Tuple<string, string, string>> WinMatrix = new List<Tuple<string, string, string>>()
                            {
                                new Tuple<string, string, string>("R", "R", "DRAW")
                                , new Tuple<string, string, string>("R", "P", "P2")
                                , new Tuple<string, string, string>("R", "S", "P1")
                                , new Tuple<string, string, string>("R", "L", "P1")
                                , new Tuple<string, string, string>("R", "V", "P2")
                                , new Tuple<string, string, string>("P", "R", "P1")
                                , new Tuple<string, string, string>("P", "P", "DRAW")
                                , new Tuple<string, string, string>("P", "S", "P2")
                                , new Tuple<string, string, string>("P", "L", "P2")
                                , new Tuple<string, string, string>("P", "V", "P1")
                                , new Tuple<string, string, string>("S", "R", "P2")
                                , new Tuple<string, string, string>("S", "P", "P1")
                                , new Tuple<string, string, string>("S", "S", "DRAW")
                                , new Tuple<string, string, string>("S", "L", "P1")
                                , new Tuple<string, string, string>("S", "V", "P2")
                                , new Tuple<string, string, string>("L", "R", "P2")
                                , new Tuple<string, string, string>("L", "P", "P1")
                                , new Tuple<string, string, string>("L", "S", "P2")
                                , new Tuple<string, string, string>("L", "L", "DRAW")
                                , new Tuple<string, string, string>("L", "V", "P1")
                                , new Tuple<string, string, string>("V", "R", "P1")
                                , new Tuple<string, string, string>("V", "P", "P2")
                                , new Tuple<string, string, string>("V", "S", "P1")
                                , new Tuple<string, string, string>("V", "L", "P2")
                                , new Tuple<string, string, string>("V", "V", "DRAW")

                            };
        public static string GetWinner(string r1, string r2)
        {

            bool r1IsValid = validAnswer.Contains(r1);
            bool r2IsValid = validAnswer.Contains(r2);

            if (!r1IsValid && !r2IsValid) return "DRAW";
            if (!r1IsValid) return "P2";
            if (!r2IsValid) return "P1";

            var winner = WinMatrix.FirstOrDefault(q => q.Item1 == r1 && q.Item2 == r2);
            if (winner != null) return winner.Item3;

            return "DRAW";
        }
    }

    public class Result
    {
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        public int P1Score { get; set; }
        public int P2Score { get; set; }
        public double P1AvgTimeMs { get; set; }
        public double P2AvgTimeMs { get; set; }
    }

    public class Player
    {
        public string Name { get; set; }
        public string PrefixCommand { get; set; }
        public string BotExecutable{ get; set; }
        public string JavaArgs { get; set; }
        public bool RequiresCompile { get; set; }
        public string Author { get; set; }
        public int LeagueScore { get; set; }
        public List<double> AvgDecisionTimes { get; set; }

        public Player()
        {
            LeagueScore = 0;
            AvgDecisionTimes = new List<double>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
