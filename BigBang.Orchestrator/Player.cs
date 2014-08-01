using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Orchestrator
{
    [DebuggerDisplay("{Name}")]
    public class Player
    {
        public static string AppDirectory
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
        }

        public static string PlayerDirectory
        {
            get { return Path.GetDirectoryName(AppDirectory) + "\\Players"; }
        }

        public string Name { get; set; }
        public string PrefixCommand { get; set; }
        public string BotExecutable { get; set; }
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
