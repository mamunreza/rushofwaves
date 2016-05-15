namespace SearchConsole
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class SimpleSeeker : ISearchable<string>
    {
        public string Search(string input)
        {
            var returnTexts = new StringBuilder();
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            foreach (var line in File.ReadLines(input)
                .Where(line => line.Contains("->") & line.EndsWith(".dll")))
            {
                returnTexts.AppendLine(line);
            }

            return returnTexts.ToString();
        }
    }
}