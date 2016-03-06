using System;

namespace GenericInterfaces
{
    public class ShowType<T> : IWork<T>
    {
        public void Do(T value)
        {
            Console.WriteLine($"{value.GetType()}");
        }
    }
    public class ShowValue<T> : IWork<T>
    {
        public void Do(T value)
        {
            Console.WriteLine($"{value}");
        }
    }
}
