using System;
using System.Linq;
using System.Net;

namespace CasinoShakespeare
{
    class CasinoShakespeare
    {
        private static void Main()
        {
            try
            {
                char[] ar = {'R', 'P', 'S', 'L', 'V'};
                var a = new WebClient().DownloadString("http://www.iheartquotes.com/api/v1/random").Select(char.ToUpperInvariant).FirstOrDefault(ar.Contains);
                Console.WriteLine(a == default(char) ? 'S' : a);
            }
            catch (Exception)
            {
                Console.WriteLine("R");
            }
        }
    }
}