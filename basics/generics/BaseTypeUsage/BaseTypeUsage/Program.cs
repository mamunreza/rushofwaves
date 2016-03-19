namespace BaseTypeUsage
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<Item>();

            var ints = new Item<int>();
            var doubles = new Item<double>();
            var strings = new Item<string>();

            items.Add(ints);
            items.Add(doubles);
            items.Add(strings);

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class Item
    {

    }

    public class Item<T> : Item
    {

    }
}
