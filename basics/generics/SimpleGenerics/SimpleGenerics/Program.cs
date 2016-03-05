using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegerValueProcessing();
            Console.WriteLine("\n");
            DifferentDataTypeProcessing();
        }

        private static void IntegerValueProcessing()
        {
            var things = new List<GenericThing<int>>();

            things.Add(new GenericThing<int>(3));
            things.Add(new GenericThing<int>(13));
            things.Add(new GenericThing<int>(98));
            things.Add(new GenericThing<int>(7));
            things.Add(new GenericThing<int>(55));

            foreach (var thing in things)
            {
                thing.Write();
            }
        }

        private static void DifferentDataTypeProcessing()
        {
            ArrayList list = new ArrayList();

            list.Add(new GenericThing<int>(3));
            list.Add(new GenericThing<string>("Hello"));
            list.Add(new GenericThing<double>(98.763));
            list.Add(new GenericThing<object>(7));
            list.Add(new GenericThing<bool>(true));

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"We have a {list[i].GetType()}");

            }
        }
    }

    public class GenericThing<T>
    {
        private T _item;
        public GenericThing(T item)
        {
            _item = item;
        }

        public void Write()
        {
            Console.WriteLine($"We have a {_item.GetType()} of value {_item}");
        }
    }
}
