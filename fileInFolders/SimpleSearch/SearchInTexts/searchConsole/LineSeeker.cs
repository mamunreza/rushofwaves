namespace SearchConsole
{
    using System.Collections.Generic;
    using System.IO;

    public class LineSeeker : ISearchable<IEnumerable<string>>
    {
        public IEnumerable<string> Search(string input)
        {
            return File.ReadLines(input);
        }
    }
}