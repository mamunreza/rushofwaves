using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericComparer
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EmployeeComparer : IEqualityComparer<Employee>, IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return string.Compare(x.Name, y.Name);
        }

        public bool Equals(Employee x, Employee y)
        {
            return string.Equals(x.Name, y.Name);
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
