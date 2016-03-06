using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegationWork
{
    public class Helper
    {
        public static void Show<T>( IList<T> list, Action<T> show)
        {
            foreach (var item in list)
            {
                show(item);
            }
        }
    }
}
