namespace SearchConsole
{
    public interface IProcessable<T>
    {
        T Process(string line);
    }
}