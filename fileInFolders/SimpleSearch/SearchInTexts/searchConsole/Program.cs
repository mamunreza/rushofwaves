using System;

namespace SearchConsole
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            //DoSimpleSeek();

            //DoAdvanceSearch();

            //DiaryWork();

            ReturnGuid();
        }

        private static void DiaryWork()
        {
            var diary = new Diary(1);
            Console.WriteLine(diary.GetPageCount());
            diary.Grow(2);
            Console.WriteLine(diary.GetPageCount());
            Console.ReadLine();
        }

        private static void DoAdvanceSearch()
        {
            var seeker = new LineSeeker();
            var processor = new LineProcessor();
            //var lineBuilder = new StringBuilder();

            using (var output = new StreamWriter(@"D:\TestDestination\General\Output.txt"))
            {
                foreach (var line in seeker.Search(@"D:\TestSource\General\BuildOrder.txt"))
                {
                    var processedLine = processor.Process(line);
                    if (processedLine.Length > 0)
                    {
                        output.WriteLine(processedLine);
                    }
                }
            }
        }

        private static void DoSimpleSeek()
        {
            var seeker = new SimpleSeeker();
            var texts = seeker.Search(@"D:\TestSource\General\BuildOrder.txt");
            Console.WriteLine(texts);
            Console.ReadLine();
        }

        public static void ReturnGuid()
        {
            Console.WriteLine(Guid.NewGuid().ToString().ToUpper());
            Console.ReadLine();
        }
    }
}
