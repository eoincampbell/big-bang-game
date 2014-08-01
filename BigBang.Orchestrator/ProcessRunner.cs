using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Orchestrator
{
    public static class ProcessRunner
    {
        public static void BuildPlayerBots(IList<Player> players)
        {
            foreach (var player in players)
            {
                if (player.RequiresCompile)
                {
                    var dir = Player.PlayerDirectory;

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

        public static string RunPlayerProcess(ref Process p, string player1ParamList, string player2ParamList, Player pl, string dir)
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
                var exec = dir + "\\" + pl.BotExecutable;
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

    }
}
