using System;
using System.Collections.Generic;

namespace SimpleGenerics
{
    public class ServiceWorker
    {
        public void SimpleHashSetWork()
        {
            HashSet<Person> set = new HashSet<Person>();

            var personOne = new Person { Name = "Aroosh" };
            var personTwo = new Person { Name = "Rahi" };
            var personThree = new Person { Name = "Adyaan" };

            set.Add(personOne);
            set.Add(personTwo);
            set.Add(personThree);
            set.Add(personTwo); // this won't be added

            foreach (var person in set)
            {
                Console.WriteLine(person.Name);
            }
        }

        public void SimpleLinkedListWork()
        {
            LinkedList<Person> list = new LinkedList<Person>();
            list.AddFirst(new Person { Name = "Adyaan" });
            list.AddLast(new Person { Name = "Aroosh" });

            var first = list.First;
            list.AddAfter(first, new Person { Name = "Rahi" });
            list.AddBefore(first, new Person { Name = "Mumu" });

            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("----------");

            list.AddLast(new Person { Name = "Someone" });

            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("----------");

            list.RemoveLast();

            var node = list.First;
            while (node != null)
            {
                Console.WriteLine(node.Value.Name);
                node = node.Next;
            }
        }

        public void SimpleDictionaryWork()
        {
            var nephewList = new Dictionary<string, List<Person>>();

            nephewList.Add("Reza",
                new List<Person>() { new Person { Name = "Adyaan Reza" } });
            nephewList.Add("Khan",
                new List<Person>() { new Person { Name = "Shehzaeb Khan" } });
            nephewList.Add("Ali", new List<Person>());

            nephewList["Reza"].Add(new Person { Name = "Aroosh Reza" });
            nephewList["Khan"].Add(new Person { Name = "Azhar Khan" });
            nephewList["Ali"].Add(new Person { Name = "Samara Ali" });
            nephewList["Ali"].Add(new Person { Name = "Saifan Ali" });

            foreach (var item in nephewList)
            {
                foreach (var nephew in item.Value)
                {
                    Console.WriteLine($"{item.Key} : {nephew.Name}");
                }
            }
        }
    }
}
