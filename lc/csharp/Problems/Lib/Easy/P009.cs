using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib.Easy;

public class P009
{
    // Using string
    // public bool IsPalindrome(int x)
    // {
    //     string myString = x.ToString();
    //     int length = myString.Length;
    //     for (int i = 0; i < length / 2; i++)
    //     {
    //         if (myString[i] != myString[length - i - 1])
    //             return false;
    //     }
    //     return true;
    // }

    public bool IsPalindrome(int x)
    {
        var reversed = 0;
        var reminder = x;
        while (reminder > 0)
        {
            var lastDigit = reminder % 10;
            reminder = reminder / 10;
            reversed = reversed * 10 + lastDigit;
        }
        return x == reversed;
    }
}