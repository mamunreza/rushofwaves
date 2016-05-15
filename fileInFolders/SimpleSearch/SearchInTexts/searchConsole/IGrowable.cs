namespace SearchConsole
{
    public interface IGrowable<in T>
    {
        void Grow(T input);
    }

    //public interface IPageGrowable : IGrowable<int>
    //{

    //}

    //public interface ILineGrowable : IGrowable<int>
    //{

    //}

    //public interface ITextGrowable : IGrowable<string>
    //{

    //}

    //public interface IGrowableInDiary : ITextGrowable, ILineGrowable, IPageGrowable
    //{

    //}
}