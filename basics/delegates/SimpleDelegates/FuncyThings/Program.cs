using System;

namespace FuncyThings
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringyJob = new Func<string, string>(Give);
            var a = stringyJob("Hello");
            a += Reverse("Hello");

            Console.WriteLine(a);
            Console.ReadLine();
        }

        public static string Give(string s)
        {
            return s;
        }

        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string Uppercase(string s)
        {
            return s.ToUpper();
        }

        public static string Lowercase(string s)
        {
            return s.ToLower();
        }
    }
}
