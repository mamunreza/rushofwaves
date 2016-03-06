using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var departments = new DepartmentCollection();

            departments.Add("Sales", new Employee { Name = "Nadim" })
                       .Add("Sales", new Employee { Name = "Zahid" })
                       .Add("Sales", new Employee { Name = "Jafor" });

            departments.Add("HR", new Employee { Name = "Atique" })
                       .Add("HR", new Employee { Name = "Hasan" })
                       .Add("HR", new Employee { Name = "Atique" });  // this won't be added to the collection

            departments.Add("IT", new Employee { Name = "Sharif" })
                       .Add("IT", new Employee { Name = "Sadik" });

            foreach (var department in departments)
            {
                Console.WriteLine(department.Key);
                foreach (var employee in department.Value)
                {
                    Console.WriteLine("\t" + employee.Name);
                }
            }
        }
    }
}
