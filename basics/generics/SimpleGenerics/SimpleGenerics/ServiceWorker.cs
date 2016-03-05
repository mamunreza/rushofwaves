using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
