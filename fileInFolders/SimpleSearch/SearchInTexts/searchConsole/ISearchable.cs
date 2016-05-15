namespace SearchConsole
{
    public interface ISearchable<T>
    {
        T Search(string input);
    }
}