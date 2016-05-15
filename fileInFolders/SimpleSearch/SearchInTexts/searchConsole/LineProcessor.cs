using System;

namespace SearchConsole
{
    using System.Globalization;
    using System.Linq;

    public class LineProcessor : IProcessable<string>
    {
        string stopper = "->";
        public string Process(string line)
        {
            if (line.Contains("Infrastructure") && line.EndsWith(".dll") /*|| line.EndsWith(".exe")*/)
            {
                return line.Substring(0, line.IndexOf(this.stopper) - 1);
            }

            return String.Empty;
        }
    }
}