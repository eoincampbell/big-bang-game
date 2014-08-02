using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Combinatorics.Collections;

namespace BigBang.Orchestrator
{
    using System.Diagnostics;
    using System.Collections.Concurrent;

    public interface IMatchGenerator
    {
        void CompleteMatch(Match match);
    }

    public class MatchGenerator : IMatchGenerator
    {
        private static object SYNC_LOCK = new object();

        private ConcurrentDictionary<Player, Player> _activePlayers;

        private ConcurrentQueue<Match> _allMatches;

        public MatchGenerator()
        {
            _activePlayers = new ConcurrentDictionary<Player, Player>();
        }

        public IEnumerable<Match> Generate(IList<Match> matches)
        {
            _allMatches = new ConcurrentQueue<Match>(matches);

            while (_allMatches.Any())
            {
                Match nextMatch;


                lock (SYNC_LOCK)
                {
                     _allMatches.TryDequeue(out nextMatch);

                    if ((!_activePlayers.ContainsKey(nextMatch.Player1) &&
                         !_activePlayers.ContainsKey(nextMatch.Player2) && _activePlayers.Count() < 50))
                    {
                        
                        //If neither player is in the active player list, then this is a good match.
                        //So add both players

                        _activePlayers.TryAdd(nextMatch.Player1, nextMatch.Player1);
                        _activePlayers.TryAdd(nextMatch.Player2, nextMatch.Player2);

                        
                    }
                    else
                    {
                        //Otherwise push this match back in to the start of the queue... FIFO should move on to next;
                        _allMatches.Enqueue(nextMatch);
                        nextMatch = null;
                    }
                }

                if (nextMatch != null)
                    yield return nextMatch;
            }
        }

        public void CompleteMatch(Match match)
        {
            Player junk1, junk2;
            lock (SYNC_LOCK)
            {
                _activePlayers.TryRemove(match.Player1, out junk1);
                _activePlayers.TryRemove(match.Player2, out junk2);
            }
            if (junk1 == null || junk2 == null)
            {
                Debug.WriteLine("Uhoh! a match came in for completion but on of the players who should have been in the active list didn't get removed");
            }
        }
    }

    public class Match
    {
        public IMatchGenerator Source { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public void IsCompleted()
        {
            Source.CompleteMatch(this);
        }
    }

    public class Program
    {
        public static object SYNC_LOCK = new object();
        public static object PROC_LOCK = new object();
        
        public static void Main(string[] args)
        {
            var gameLogDirectory = string.Format(@"C:\Temp\BigBang\{0:yyyyMMddHHmm}\", DateTime.Now);
            Directory.CreateDirectory(gameLogDirectory);
            using (var sw = new StreamWriter(Path.Combine(gameLogDirectory, "Tournament.log"), false))
            {
                //ProcessRunner.BuildPlayerBots(PlayerConfig.Players);
                
                var tourneyTimer = new Stopwatch();
                tourneyTimer.Start();


                var mg = new MatchGenerator();

                var matches = new Combinations<Player>(PlayerConfig.Players, 2, GenerateOption.WithoutRepetition)

                    .Select(f => new Match()
                    {
                        Player1 = f[0],
                        Player2 = f[1],
                        //Source = mg
                    }).ToList();

                var results = new List<Result>();

                foreach (var match in matches)
                {
                    PlayMatch(match, gameLogDirectory, results);
                }


                //Parallel.ForEach(mg.Generate(matches),
                //    new ParallelOptions()
                //    {
                //        MaxDegreeOfParallelism = 8
                //    },
                //    match =>
                //    {
                //        var localMatch = match;
                //        try
                //        {
                //            PlayMatch(localMatch, gameLogDirectory, results);
                //        }
                //        finally
                //        {
                //            localMatch.IsCompleted();
                //        }
                //    });

                foreach (var r in results)
                {
                    sw.WriteLine(r.GetLogOuput());
                    var p1 = PlayerConfig.Players.First(p => p == r.P1);
                    var p2 = PlayerConfig.Players.First(p => p == r.P2);

                    if (r.P1Score > r.P2Score)
                    {
                        p1.LeagueScore++;
                        p1.Wins++;
                        p2.Loss++;
                    }
                    else if (r.P1Score < r.P2Score)
                    {
                        p2.LeagueScore++;
                        p1.Loss++;
                        p2.Wins++;
                    }
                    else
                    {
                        p1.Draw++;
                        p2.Draw++;
                    }

                    p1.AvgDecisionTimes.Add(r.P1AvgTimeMs);
                    p2.AvgDecisionTimes.Add(r.P2AvgTimeMs);

                    sw.Write(r.GetLogOuput());
                }

                var resultGrid = PlayerConfig.GetOrderedPlayerResults();

                tourneyTimer.Stop();

                var printResults = PrintResultGrid(resultGrid);
                sw.WriteLine(printResults);
                sw.WriteLine("Total Players: {0}", PlayerConfig.Players.Count);
                sw.WriteLine("Total Matches Completed: {0}", results.Count);
                sw.WriteLine("Total Tourney Time: {0}", tourneyTimer.Elapsed);

                Console.WriteLine(printResults);
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void PlayMatch(Match match, string gameLogDirectory, List<Result> results)
        {
            Player p1 = null, p2 = null;
            try
            {
                if (match.Player1 == null || match.Player2 == null)
                {
                    throw new ApplicationException("One or more players didn't show!");
                }
                p1 = match.Player1;
                p2 = match.Player2;

                var result = Play(p1, p2, gameLogDirectory);
                results.Add(result);

                var resultMessage = string.Format("Result: {0} vs {1}: {2} - {3}",
                    result.P1,
                    result.P2,
                    result.P1Score,
                    result.P2Score);
                
                    result.WriteLine("| ");
                    result.WriteLine("| {0}", resultMessage);
                
                Console.WriteLine(resultMessage);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static string PrintResultGrid(IEnumerable<Player> resultGrid )
        {
            var sb = new StringBuilder();

            sb.AppendFormat("| {8} | {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} |\n",
                "Author".PadRight(20),
                "Name".PadRight(40),
                "Language".PadRight(10),
                "Score".PadRight(5),
                "Win".PadRight(5),
                "Draw".PadRight(5),
                "Loss".PadRight(5),
                "Avg. Dec. Time".PadRight(10),
                "Pos #".PadRight(5));

            sb.AppendLine("+-------+----------------------+------------------------------------------+" +
                "------------+-------+-------+-------+-------+----------------+");
            
            foreach (var rg in resultGrid)
            {
                sb.AppendFormat("| {8} | {0} | {1} | {7} | {2:000}   | {3:000}   | {4:000}   | {5:000}   | {6:0000.00} ms     |\n",
                    rg.Author.PadRight(20),
                    rg.Name.PadRight(40),
                    rg.LeagueScore,
                    rg.Wins,
                    rg.Draw,
                    rg.Loss,
                    rg.AvgDecisionTimes.Any() ? rg.AvgDecisionTimes.Average() : 0,
                    rg.Language.PadRight(10),
                    rg.Position.PadRight(5));
            }

            sb.AppendFormat("+-------+----------------------+------------------------------------------+" +
                "------------+-------+-------+-------+-------+----------------+");

            return sb.ToString();
        }

       
        

        public static Result Play(Player p1, Player p2, string gameLogDirectory)
        {
            var dir = Player.PlayerDirectory;

            var result = new Result() { P1 = p1, P2 = p2, P1Score = 0, P2Score = 0 };
            string player1ParamList = string.Empty, player2ParamList = string.Empty;
            List<long> p1Times = new List<long>(), p2Times = new List<long>();
            Stopwatch sw1 = new Stopwatch(), sw2 = new Stopwatch(), swGame = new Stopwatch();
            var sb = new StringBuilder();

            var proc = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false, RedirectStandardOutput = true, WorkingDirectory = dir
                }
            };

            swGame.Start();
            sb.AppendLine("+--------------------------------------------------------------------------------------------+");
            sb.AppendFormat("| Starting Game between {0} & {1} \n", p1.Name, p2.Name);
            sb.AppendLine("| ");
            for (var i = 0; i < 100; i++)
            {
                sw1.Reset();
                sw1.Start();
                var o1 = ProcessRunner.RunPlayerProcess(ref proc, player1ParamList, player2ParamList, p1, dir);
                sw1.Stop();
                p1Times.Add(sw1.ElapsedMilliseconds);

                //System.Threading.Thread.Sleep(1);

                sw2.Reset();
                sw2.Start();
                var o2 = ProcessRunner.RunPlayerProcess(ref proc, player2ParamList, player1ParamList, p2, dir);
                sw2.Stop();
                p2Times.Add(sw2.ElapsedMilliseconds);


                var whoWon = GetWinner(o1, o2, ref player1ParamList, ref player2ParamList);
                var whoWonMessage = "Draw Match";
                if (whoWon == "P1")
                {
                    result.P1Score++;
                    whoWonMessage = string.Format("{0} wins", p1.Name);
                }
                else if (whoWon == "P2")
                {
                    result.P2Score++;
                    whoWonMessage = string.Format("{0} wins", p2.Name);
                }

                sb.AppendFormat("| {0} plays {1} | {2} plays {3} | {4}\n", p1.Name, o1, p2.Name, o2, whoWonMessage);

            }
            swGame.Stop();
            sb.AppendLine("| ");
            sb.AppendFormat("| Game Time: {0}", swGame.Elapsed);

            result.WriteLine(sb.ToString());
            
            var resultMessage = string.Format("Result: {0} vs {1}: {2} - {3}",
                            result.P1,
                            result.P2,
                            result.P1Score,
                            result.P2Score);

            sb.AppendLine("| ");
            sb.AppendFormat("| {0}", resultMessage);

          
            
            using (var p1sw = new StreamWriter(Path.Combine(gameLogDirectory, p1.Name + ".log"), true))
            {
                p1sw.WriteLine(sb.ToString());
            }
            using (var p2sw = new StreamWriter(Path.Combine(gameLogDirectory, p2.Name + ".log"), true))
            {
                p2sw.WriteLine(sb.ToString());
            }

            result.P1AvgTimeMs = p1Times.Average();
            result.P2AvgTimeMs = p2Times.Average();

            return result;
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

        public static string GetWinner(string r1, string r2, ref string player1ParamList, ref string player2ParamList)
        {
            bool r1IsValid = validAnswer.Contains(r1);
            bool r2IsValid = validAnswer.Contains(r2);

            if (!r1IsValid && !r2IsValid)
                return "DRAW";
            
            if (!r1IsValid)
                return "P2";
            
            if (!r2IsValid)
                return "P1";

            player1ParamList += r1;
            player2ParamList += r2;

            var winner = WinMatrix.FirstOrDefault(q => q.Item1 == r1 && q.Item2 == r2);
            if (winner != null) return winner.Item3;

            return "DRAW";
        }
    }
}
