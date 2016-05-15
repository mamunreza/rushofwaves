namespace SearchConsole
{
    public class Diary : IGrowable<int>
    {
        private int pageCount;

        public Diary(int pageCount)
        {
            this.pageCount = pageCount;
        }

        public void Grow(int input)
        {
            this.pageCount += input;
        }

        public int GetPageCount()
        {
            return this.pageCount;
        }
    }
}