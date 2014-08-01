using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Orchestrator
{
    public static class PlayerConfig
    {
        public static IList<Player> Players
        {
            get { return _players; }
        }

        public static IEnumerable<Player> GetOrderedPlayerResults()
        {
            var resultGrid = Players
                    .OrderByDescending(p => p.LeagueScore)
                    .Select((v, i) =>
                    {
                        v.Position = Humanizer.OrdinalizeExtensions.Ordinalize(Players.Count(x => x.LeagueScore > v.LeagueScore) + 1);
                        return v;
                    });

            return resultGrid;
        }

        private static List<Player> _players = new List<Player>(){
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
    }
}
