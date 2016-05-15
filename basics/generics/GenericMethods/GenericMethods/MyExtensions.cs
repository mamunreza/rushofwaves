using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMethods
{
    public delegate void Printer<T>(T data);
    public static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static void Increase<T>(this IList<T> list)
        {
            int n = list.Count;
            var converter = TypeDescriptor.GetConverter(typeof(T));
            while (n > 0)
            {
                n--;
                list[n] = Helper.Add(list[n], (T)converter.ConvertTo(5, typeof(T)));
            }
        }
        public static void CompleteFullName(this IList<Person> list, string lastName)
        {
            foreach (var person in list)
            {
                person.Name += " " + lastName;
            }
        }
        public static void Show<T>(this IList<T> list, Printer<T> print)
        {
            foreach (var item in list)
            {
                print(item);
            }
        }
    }
}
