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
    }
}
