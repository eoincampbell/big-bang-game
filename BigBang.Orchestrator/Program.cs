using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinatorics.Collections;

namespace BigBang.Orchestrator
{
    using System.Diagnostics;

    public class Program
    {
        public static object SYNC_LOCK = new object();
        public static object PROC_LOCK = new object();
        
        public static List<Player> players = new List<Player>(){
                   //.NET
                    new Player { Author = "EoinC", Language = ".NET", Name = "SimpleRandomBot", BotExecutable = @"SimpleRandomBot\SimpleRandomBot.exe", RequiresCompile = true},
                    new Player { Author = "HuddleWolf", Language = ".NET", Name = "HuddleWolfTheConqueror", BotExecutable = @"HuddleWolfTheConqueror\HuddleWolfTheConqueror.exe", RequiresCompile = true},
                    new Player { Author = "ProgramFOX", Language = ".NET", Name = "Echo", BotExecutable = @"Echo\Echo.exe" , RequiresCompile = true},
                    new Player { Author = "Mikey Mouse", Language = ".NET", Name = "LizardsRule", BotExecutable = @"LizardsRule\LizardsRule.exe" , RequiresCompile = true},
////OFFLINE RUN     //new Player { Author = "Daniel", Language = ".NET", Name = "CasinoShakespeare", BotExecutable = @"CasinoShakespeare\CasinoShakespeare.exe" , RequiresCompile = true},
                   
                    //JAVA
                    new Player { Author = "Stranjyr", Language = "Java", Name = "ToddlerProof", JavaArgs = "ToddlerProof", BotExecutable = @"ToddlerProof", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },
                    new Player { Author = "kaine", Language = "Java", Name = "BoringBot", JavaArgs = "BoringBot", BotExecutable = @"BoringBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true},
                    new Player { Author = "Stretch Maniac", Language = "Java", Name = "SmartBot", JavaArgs = "SmartBot", BotExecutable = @"SmartBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },
                    new Player { Author = "Milo", Language = "Java", Name = "DogeBot", JavaArgs = "DogeBot", BotExecutable = @"DogeBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },
                    new Player { Author = "JoshDM", Language = "Java", Name = "SelfLoathingBot", JavaArgs = "SelfLoathingBot", BotExecutable = @"SelfLoathingBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },
                    new Player { Author = "JoshDM", Language = "Java", Name = "SelfHatingBot", JavaArgs = "SelfHatingBot", BotExecutable = @"SelfHatingBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },
                    new Player { Author = "Qwix", Language = "Java", Name = "Analyst", JavaArgs = "Analyst", BotExecutable = @"Analyst", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe" , RequiresCompile = true},
                    new Player { Author = "Stretch Maniac", Language = "Java", Name = "SmarterBot", JavaArgs = "SmarterBot", BotExecutable = @"SmarterBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },
                    new Player { Author = "Milo", Language = "Java", Name = "DogeBotv2", JavaArgs = "DogeBotv2", BotExecutable = @"DogeBotv2", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },

                    //Ruby
                    new Player { Author = "William Barbosa", Language = "Ruby", Name = "StarWarsFan", BotExecutable = @"StarWarsFan\StarWarsFan.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Martin Büttner", Language = "Ruby", Name = "ConservativeBot", BotExecutable = @"ConservativeBot\ConservativeBot.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},
                    new Player { Author = "Martin Büttner", Language = "Ruby", Name = "SlowLizard", BotExecutable = @"SlowLizard\SlowLizard.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Martin Büttner", Language = "Ruby", Name = "FairBot", BotExecutable = @"FairBot\FairBot.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Martin Büttner", Language = "Ruby", Name = "Vulcan", BotExecutable = @"Vulcan\Vulcan.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Thaylon", Language = "Ruby", Name = "NitPicker", BotExecutable = @"NitPicker\NitPicker.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Martin Büttner", Language = "Ruby", Name = "Markov", BotExecutable = @"Markov\Markov.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "histocrat", Language = "Ruby", Name = "Analogizer", BotExecutable = @"Analogizer\Analogizer.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
//// BROKEN         new Player { Author = "Rory O'Kane", Language = "Ruby", Name = "RandomlyWeighted", BotExecutable = @"RandomlyWeighted\RandomlyWeighted.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "histocrat", Language = "Ruby", Name = "WereVulcan", BotExecutable = @"WereVulcan\WereVulcan.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "histocrat", Language = "Ruby", Name = "BartBot", BotExecutable = @"BartBot\BartBot.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
////WITHDRAWN       new Player { Author = "histocrat", Language = "Ruby", Name = "LoopholeAbuser", BotExecutable = @"LoopholeAbuser\LoopholeAbuser.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "histocrat", Language = "Ruby", Name = "Alternator", BotExecutable = @"Alternator\Alternator.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Thaylon", Language = "Ruby", Name = "UniformBot", BotExecutable = @"UniformBot\UniformBot.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Dr R Dizzle", Language = "Ruby", Name = "BartSimpson", BotExecutable = @"BartSimpson\BartSimpson.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Dr R Dizzle", Language = "Ruby", Name = "LisaSimpson", BotExecutable = @"LisaSimpson\LisaSimpson.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "jmite", Language = "Ruby", Name = "IocainePowder", BotExecutable = @"IocainePowder\IocainePowder.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Dr R Dizzle", Language = "Ruby", Name = "Khaleesi", BotExecutable = @"Khaleesi\Khaleesi.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Dr R Dizzle", Language = "Ruby", Name = "EdwardScissorHands", BotExecutable = @"EdwardScissorHands\EdwardScissorHands.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    
                    //PYTHON 3
                    new Player { Author = "Timmy", Language = "Python3", Name = "DynamicBot", BotExecutable = @"DynamicBot\DynamicBot.py", PrefixCommand = @"C:\Python34\python.exe"},                    
                    new Player { Author = "Kyle Kanos", Language = "Python3", Name = "ViolentBot", BotExecutable = @"ViolentBot\ViolentBot.py", PrefixCommand = @"C:\Python34\python.exe"},                    
                    new Player { Author = "Kyle Kanos", Language = "Python3", Name = "LexicographicBot", BotExecutable = @"LexicographicBot\LexicographicBot.py", PrefixCommand = @"C:\Python34\python.exe"},                    
                    new Player { Author = "Trimsty", Language = "Python3", Name = "Herpetologist", BotExecutable = @"Herpetologist\Herpetologist.py", PrefixCommand = @"C:\Python34\python.exe"},                    
                        
                    //PYTHON 2
                    new Player { Author = "bitpwner", Language = "Python2", Name = "AlgorithmBot", BotExecutable = @"AlgorithmBot\AlgorithmBot.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "undergroundmonorail", Language = "Python2", Name = "TheGambler", BotExecutable = @"TheGambler\TheGambler.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "Emil", Language = "Python2", Name = "Pony", BotExecutable = @"Pony\Pony.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "Claudiu", Language = "Python2", Name = "SuperMarkov", BotExecutable = @"SuperMarkov\SuperMarkov.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "cipher", Language = "Python2", Name = "LemmingBot", BotExecutable = @"LemmingBot\LemmingBot.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "Docopoper", Language = "Python2", Name = "RoboticOboeBotOboeTuner", BotExecutable = @"RoboticOboeBotOboeTuner\RoboticOboeBotOboeTuner.py", PrefixCommand = @"C:\Python27\python.exe"},                    



                    //LUA
                    new Player { Author = "William Barbosa", Language = "Lua", Name = "BarneyStinson", BotExecutable = @"BarneyStinson\BarneyStinson.lua", PrefixCommand = @"C:\Program Files (x86)\Lua\5.1\lua.exe"},

                    //PHP
                    new Player { Author = "ArcticanAudio", Language = "PHP", Name = "SpockOrRock", BotExecutable = @"SpockOrRock\SpockOrRock.php", PrefixCommand = @"C:\PHP5.15\php.exe"} ,                  
//// BROKEN         new Player { Author = "Hikata Ikaruga", Language = "PHP", Name = "CounterPreferenceBot", BotExecutable = @"CounterPreferenceBot\CounterPreferenceBot.php", PrefixCommand = @"C:\PHP5.15\php.exe"} ,                  

                    //PERL
                    new Player { Author = "PhiNotPi", Language = "Perl", Name = "BayesianBot", BotExecutable = @"BayesianBot\BayesianBot.perl", PrefixCommand = @"C:\strawberry\perl\bin\perl.exe"},                   
                    new Player { Author = "killmous", Language = "Perl", Name = "MAWBRBot", BotExecutable = @"MAWBRBot\MAWBRBot.perl", PrefixCommand = @"C:\strawberry\perl\bin\perl.exe"},                 

                    //HASKELL
                    new Player { Author = "DrJPepper", Language = "Haskel", Name = "MonadBot", BotExecutable = @"MonadBot\MonadBot.exe" , RequiresCompile = true},

                    //SH (Cygwin)
//// BROKEN         new Player { Author = "mccannf", Language = "Bash", Name = "BashRocksBot", BotExecutable = @"BashRocksBot\BashRocksBot.sh", PrefixCommand = @"C:\Cygwin\bin\bash.exe"},        
    
                    //JS (Node)
                    new Player { Author = "mccannf", Language = "JS", Name = "YAARBot", BotExecutable = @"YAARBot\YAARBot.js", PrefixCommand = @"C:\Chocolatey\lib\nodejs.commandline.0.10.29\tools\node.exe"},                    

                    //Cobra
                    new Player { Author = "Ourous", Language = "Cobra", Name = "Q", BotExecutable = @"Q\Q.exe", RequiresCompile = true},                    
                    new Player { Author = "Ourous", Language = "Cobra", Name = "QQ", BotExecutable = @"QQ\QQ.exe",RequiresCompile = true},                  
                    new Player { Author = "Ourous", Language = "Cobra", Name = "DejaQ", BotExecutable = @"DejaQ\DejaQ.exe",RequiresCompile = true},                    
                    new Player { Author = "Ourous", Language = "Cobra", Name = "GitGudBot", BotExecutable = @"GitGudBot\GitGudBot.exe",RequiresCompile = true},                    
 
                    //LISP


                    new Player { Author = "ja72", Language = ".NET", Name = "BlindForesight", BotExecutable = @"BlindForesight\BlindForesight.exe" , RequiresCompile = true},
                    new Player { Author = "Dr R Dizzle", Language = "Ruby", Name = "BetterLisaSimpson", BotExecutable = @"BetterLisaSimpson\BetterLisaSimpson.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Docopoper", Language = "Python2", Name = "ConcessionBot", BotExecutable = @"ConcessionBot\ConcessionBot.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "Docopoper", Language = "Python2", Name = "OboeBeater", BotExecutable = @"OboeBeater\OboeBeater.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "Docopoper", Language = "Python2", Name = "OboeOboeBeater", BotExecutable = @"OboeOboeBeater\OboeOboeBeater.py", PrefixCommand = @"C:\Python27\python.exe"},                    

                    new Player { Author = "Carlos Martinez", Language = "Java", Name = "EasyGame", JavaArgs = "EasyGame", BotExecutable = @"EasyGame", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },
                    new Player { Author = "Roy van Rijn", Language = "Java", Name = "Gazzr", JavaArgs = "Gazzr", BotExecutable = @"Gazzr", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe" , RequiresCompile = true},
                    new Player { Author = "Josef E.", Language = "Java", Name = "OneBehind", JavaArgs = "OneBehind", BotExecutable = @"OneBehind", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },

                    new Player { Author = "undergroundmonorail", Language = "Python2", Name = "TheGamblersBrother", BotExecutable = @"TheGamblersBrother\TheGamblersBrother.py", PrefixCommand = @"C:\Python27\python.exe"},                    

                    new Player { Author = "Thaylon", Language = "Ruby", Name = "Naan", BotExecutable = @"Naan\Naan.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Thaylon", Language = "Ruby", Name = "NaanViolence", BotExecutable = @"NaanViolence\NaanViolence.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    
                    new Player { Author = "Stranjyr", Language = "Python2", Name = "RelaxedBot", BotExecutable = @"RelaxedBot\RelaxedBot.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "histocrat", Language = "Ruby", Name = "LeonardShelby", BotExecutable = @"LeonardShelby\LeonardShelby.rb", PrefixCommand = @"C:\Ruby200-x64\bin\ruby.exe"},                    

                    new Player { Author = "kaine", Language = "Java", Name = "ExcitingishBot", JavaArgs = "ExcitingishBot", BotExecutable = @"ExcitingishBot", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe" , RequiresCompile = true},
                    new Player { Author = "Luis Mars", Language = "Java", Name = "Botzinga", JavaArgs = "Botzinga", BotExecutable = @"Botzinga", PrefixCommand = @"C:\Program Files\Java\jre8\bin\java.exe", RequiresCompile = true },

                    new Player { Author = "NonFunctional User29916", Language = "Lisp", Name = "IHaveNoIdeaWhatImDoing", BotExecutable = @"IHaveNoIdeaWhatImDoing\IHaveNoIdeaWhatImDoing.exe" , RequiresCompile = true},
                    new Player { Author = "Emil", Language = "Python2", Name = "Dienstag", BotExecutable = @"Dienstag\Dienstag.py", PrefixCommand = @"C:\Python27\python.exe"},                    
                    new Player { Author = "john smith", Language = "PHP", Name = "RAMBOBot", BotExecutable = @"RAMBOBot\RAMBOBot.php", PrefixCommand = @"C:\PHP5.15\php.exe"} ,                  
                    new Player { Author = "robotik", Language = "Lua", Name = "Evolver", BotExecutable = @"Evolver\Evolver.lua", PrefixCommand = @"C:\Program Files (x86)\Lua\5.1\lua.exe"},

                    new Player { Author = "ovenror", Language = "Python2", Name = "TobiasFuenke", BotExecutable = @"TobiasFuenke\TobiasFuenke.py", PrefixCommand = @"C:\Python27\python.exe"},                    

                };
        

        public static string AppDirectory
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
        }

        public static string PlayerDirectory
        {
            get { return Path.GetDirectoryName(AppDirectory) + "\\Players"; }
        }

        static void Main(string[] args)
        {
            var gameLogDirectory = string.Format(@"C:\Temp\BigBang\{0:yyyyMMddHHmm}\", DateTime.Now);
            Directory.CreateDirectory(gameLogDirectory);
            using (StreamWriter sw = new StreamWriter(Path.Combine(gameLogDirectory, "Tournament.log"), false))
            {
                //BuildWhereRequired(players);
                
                var tourneyTimer = new Stopwatch();
                tourneyTimer.Start();
                
                var matches = new Combinations<Player>(players, 2, GenerateOption.WithoutRepetition);
                var results = new List<Result>();

                foreach (var match in matches)
                {
                    PlayMatch(match, sw, gameLogDirectory, results);
                }

                //Parallel.ForEach(matches, 
                //    new ParallelOptions() { MaxDegreeOfParallelism = 8 } ,
                //    match =>
                //{
                //    PlayMatch(match, sw, gameLogDirectory, results);
                //});

                foreach (var r in results)
                {
                    var p1 = players.First(p => p == r.P1);
                    var p2 = players.First(p => p == r.P2);

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
                }


                var resultGrid = players
                    .OrderByDescending(p => p.LeagueScore)
                    .Select((v, i) =>
                    {
                        v.Position = Humanizer.OrdinalizeExtensions.Ordinalize(players.Count(x => x.LeagueScore > v.LeagueScore) + 1);
                        return v;
                    });


                tourneyTimer.Stop();
                

                var printResults = PrintResultGrid(resultGrid);
                sw.WriteLine(printResults);
                sw.WriteLine("Total Players: {0}", players.Count);
                sw.WriteLine("Total Matches Completed: {0}", matches.Count);
                sw.WriteLine("Total Tourney Time: {0}", tourneyTimer.Elapsed);

                Console.WriteLine(printResults);
            }

            //var logdir = Path.GetFullPath(@"..\..\..\tourneys\");
            //File.Copy("Tournament.log",
            //    Path.Combine(logdir, string.Format("Tournament-{0:yyyy-MM-dd-HH-mm-ss}.txt", DateTime.Now)));
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void PlayMatch(IList<Player> match, StreamWriter sw, string gameLogDirectory, List<Result> results)
        {
            Player p1 = null, p2 = null;
            try
            {
                if (match[0] == null || match[1] == null)
                {
                    throw new ApplicationException("One or more players didn't show!");
                }
                p1 = match[0];
                p2 = match[1];

                var result = Play(p1, p2, sw, gameLogDirectory);
                results.Add(result);

                var resultMessage = string.Format("Result: {0} vs {1}: {2} - {3}",
                    result.P1,
                    result.P2,
                    result.P1Score,
                    result.P2Score);

                
                    sw.WriteLine("| ");
                    sw.WriteLine("| {0}", resultMessage);
                
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
                    rg.AvgDecisionTimes.Average(),
                    rg.Language.PadRight(10),
                    rg.Position.PadRight(5));
            }

            sb.AppendFormat("+-------+----------------------+------------------------------------------+" +
                "------------+-------+-------+-------+-------+----------------+");

            return sb.ToString();
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

        public static Result Play(Player p1, Player p2, StreamWriter sw, string gameLogDirectory)
        {
            var dir = PlayerDirectory;

            var result = new Result() { P1 = p1, P2 = p2, P1Score = 0, P2Score = 0 };
            var player1ParamList = string.Empty;
            var player2ParamList = string.Empty;
            var p1Times = new List<long>();
            var p2Times = new List<long>();
            var sw1 = new Stopwatch();
            var sw2 = new Stopwatch();
            var swGame = new Stopwatch();
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
                var o1 = RunProcess(ref proc, player1ParamList, player2ParamList, p1, dir, sw);
                sw1.Stop();
                p1Times.Add(sw1.ElapsedMilliseconds);

                //System.Threading.Thread.Sleep(1);

                sw2.Reset();
                sw2.Start();
                var o2 = RunProcess(ref proc, player2ParamList, player1ParamList, p2, dir, sw);
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



            
                sw.WriteLine(sb.ToString());
            
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

        public static string RunProcess(ref Process p, string player1ParamList, string player2ParamList, Player pl, string dir, StreamWriter sw)
        {
            
            if (!string.IsNullOrEmpty(pl.PrefixCommand))
            {
                var exec = string.Format("\"{0}\\{1}\"", dir, pl.BotExecutable);
                p.StartInfo.FileName = pl.PrefixCommand;

                if (!string.IsNullOrEmpty(pl.JavaArgs))
                {
                    var args = string.Format("-cp \"{0}\\{1}\" {2}", dir, pl.JavaArgs, pl.BotExecutable);
                    p.StartInfo.Arguments = string.IsNullOrEmpty(player1ParamList)
                        ? args
                        : string.Format("{0} {1} {2}", args, player1ParamList, player2ParamList);
                }
                else
                {

                    p.StartInfo.Arguments = string.IsNullOrEmpty(player1ParamList)
                        ? exec
                        : string.Format("{0} {1} {2}", exec, player1ParamList, player2ParamList);

                }
            }
            else
            {
                var exec =  dir + "\\" + pl.BotExecutable;
                p.StartInfo.FileName = exec;
                p.StartInfo.Arguments = string.IsNullOrEmpty(player1ParamList)
                    ? string.Empty
                    : string.Format("{0} {1}", player1ParamList, player2ParamList);
            }

            string output = string.Empty;
            
                p.Start();
                output = p.StandardOutput.ReadToEnd().Trim();
                p.WaitForExit();
            

            return output.Length > 1
                ? output.Substring(0, 1)
                : output;
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
        public int Wins { get; set; }
        public int Loss { get; set; }
        public int Draw { get; set; }
        public string Position { get; set; }
        public string Language { get; set; }
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
