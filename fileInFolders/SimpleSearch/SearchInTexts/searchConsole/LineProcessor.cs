using System;

namespace SearchConsole
{
    using System.Globalization;
    using System.Linq;
    using System.Net.NetworkInformation;

    public class LineProcessor : IProcessable<string>
    {
        private static int prefix = 0;
        private string stopper = "->";

        public string Process(string line)
        {
            if (line.Contains("Infrastructure") && line.EndsWith(".dll") /*|| line.EndsWith(".exe")*/)
            {
                ++prefix;
                return prefix + " " + line.Substring(0, line.IndexOf(this.stopper) - 1);
            }

            return String.Empty;
        }
    }
}