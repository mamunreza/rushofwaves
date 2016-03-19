using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            //WinningNumbers();
            //IncreaseIntegerListValues();
            //IncreaseDoubleListValues();
            //AddFamilyNameToPersons();
            HeroesForever();
        }
        /// <summary>
        /// This will call a generic shuffle method
        /// </summary>
        private static void WinningNumbers()
        {
            var numbers = new List<int>(Enumerable.Range(1, 75));
            numbers.Shuffle();
            Console.WriteLine("The winning numbers are: {0}", string.Join(",  ", numbers.GetRange(0, 5)));
        }
        /// <summary>
        /// Increases the values of a generic list of integers
        /// </summary>
        private static void IncreaseIntegerListValues()
        {
            var numbers = new List<int>(Enumerable.Range(1, 10));
            Console.WriteLine("Before: \n{0}", string.Join(",  ", numbers.GetRange(0, 9)));
            numbers.Increase();
            Console.WriteLine("After: \n{0}", string.Join(",  ", numbers.GetRange(0, 9)));
        }
        /// <summary>
        /// Increases the values of a generic list of doubles
        /// </summary>
        private static void IncreaseDoubleListValues()
        {
            var numbers = new List<double> { 20.40, 7.13, 5.0, 11.13, 8.7 };
            Console.WriteLine("Before: \n{0}", string.Join(",  ", numbers.GetRange(0, 4)));
            numbers.Increase();
            Console.WriteLine("After: \n{0}", string.Join(",  ", numbers.GetRange(0, 4)));
        }
        /// <summary>
        /// Adds last name to each persons name in an existing list of persons
        /// </summary>
        private static void AddFamilyNameToPersons()
        {
            var persons = new List<Person>
            {
                new Person { Name="Meherunnisa"},
                new Person { Name="Meshkat"},
                new Person { Name="Mardad"},
                new Person { Name="Shawgat"},
                new Person { Name="Nishat"}
            };

            foreach (var person in persons)
            {
                Console.WriteLine(person.Name);
            }
            Console.WriteLine("");
            persons.CompleteFullName("Sheikh");
            foreach (var person in persons)
            {
                Console.WriteLine(person.Name);
            }
        }
        /// <summary>
        /// Shows every persons name from a list of persons
        /// </summary>
        private static void HeroesForever()
        {
            var numbers = new List<int>(Enumerable.Range(1, 7));
            numbers.Show(ConsoleWriteGenericNames);
            numbers.Show(ConsoleWriteGenericTypes);

            var persons = new List<Person>
            {
                new Person {Name = "Mohiuddin Jahangir"},
                new Person {Name = "Hamidur Rahman"},
                new Person {Name = "Mostafa Kamal"},
                new Person {Name = "Mohammad Ruhul Amin"},
                new Person {Name = "Matiur Rahman"},
                new Person {Name = "Munshi Abdur Rouf"},
                new Person {Name = "Noor Mohammad"}
            };
            persons.Show(ConsoleWriteGenericNames);
            persons.Show(ConsoleWriteGenericTypes);
        }
        static void ConsoleWriteGenericNames(int value)
        {
            Console.WriteLine(value);
        }
        static void ConsoleWriteGenericNames(Person person)
        {
            Console.WriteLine(person.Name);
        }
        static void ConsoleWriteGenericTypes(int value)
        {
            Console.WriteLine(value.GetType());
        }
        static void ConsoleWriteGenericTypes(Person person)
        {
            Console.WriteLine(person.GetType());
        }
    }
}
