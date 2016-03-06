using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegationWork
{
    public delegate void Work();
    class Program
    {
        static void Main(string[] args)
        {
            //NumberActions();
            //DoubleActions();
            //FuncyPredicatedActions();

            //Action<int> printInteger = a => Console.WriteLine(a);
            //Action<double> printDouble = d => Console.WriteLine(d);

            //printInteger(5);
            //printInteger(4);
            //printInteger(44);
            //printInteger(334);
            //printInteger(11);
            //printDouble(5.9);
            //printDouble(53.9);
            //printDouble(25.9);

            //Action<bool> printAnother = d => FuncyPredicatedActions();
            //printAnother(true);

            Work someWork = FuncyPredicatedActions;
            someWork += DoubleActions;
            someWork();
        }

        private static void NumberActions()
        {
            var numbers = new List<int>(Enumerable.Range(1, 10));

            Action<int> print = d => Console.WriteLine(d);
            Action<int> printInSingleLine = d => Console.Write(d + " ");

            Helper.Show(numbers, print);
            Helper.Show(numbers, printInSingleLine);
        }
        private static void DoubleActions()
        {
            var doubles = new List<double> { 20.40, 7.13, 5.0, 11.13, 8.7 };

            Action<double> print = d => Console.WriteLine(d);
            Action<double> printInSingleLine = d => Console.Write(d + " ");

            Helper.Show(doubles, print);
            Helper.Show(doubles, printInSingleLine);
        }
        private static void FuncyPredicatedActions()
        {
            Action<bool> print = d => Console.WriteLine(d);
            Func<int, int> square = f => f * f;
            Func<int, int, int> add = (x, y) => x + y;
            Predicate<int> isLessThanTen = d => d < 10;

            Console.WriteLine(square(4));
            Console.WriteLine(add(4, 5));
            Console.WriteLine("---------");
            print(isLessThanTen(square(add(2, 3))));
        }
    }
}
