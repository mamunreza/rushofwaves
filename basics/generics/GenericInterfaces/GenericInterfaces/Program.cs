using System.Collections.Generic;

namespace GenericInterfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IWork<int>> intList = new List<IWork<int>>();
            var intWork1 = new ShowType<int>();
            var intWork2 = new ShowValue<int>();
            intList.Add(intWork1);
            intList.Add(intWork2);
            foreach (var item in intList)
            {
                item.Do(5);
            }

            //Console.WriteLine("\n");

            List<IWork<double>> doubleList = new List<IWork<double>>();
            var doubleWork1 = new ShowType<double>();
            var doubleWork2 = new ShowValue<double>();
            doubleList.Add(doubleWork1);
            doubleList.Add(doubleWork2);
            foreach (var item in doubleList)
            {
                item.Do(5.87);
            }

            List<IWork<Person>> personList = new List<IWork<Person>>();
            var personWork1 = new ShowType<Person>();
            var personWork2 = new ShowValue<Person>();
            personList.Add(personWork1);
            personList.Add(personWork2);
            foreach (var item in personList)
            {
                item.Do(new Person { Name = "Arys" });
            }
        }
    }
}
