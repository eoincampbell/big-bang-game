using System;
using System.IO;

namespace BigBang.Orchestrator
{
    public class Result
    {
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        public int P1Score { get; set; }
        public int P2Score { get; set; }
        public double P1AvgTimeMs { get; set; }
        public double P2AvgTimeMs { get; set; }

        public Exception Exception { get; set; }

        private StringWriter _sw = new StringWriter();

        public void WriteLine(string formatString, params string[] args)
        {
            _sw.WriteLine(formatString, args);
        }

        public string GetLogOuput()
        {
            if (Exception != null)
            {
                _sw.WriteLine(Exception);
            }

            return _sw.ToString();
        }
    }
}