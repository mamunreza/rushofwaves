using Lib.Easy;

namespace Problems.Cnsl;

internal static class RunnerEasy
{
    internal static void ExecuteP001()
    {
        int[] nums = { 2, 7, 11, 15 };
        var p = new P001();
        var result = p.TwoSum(nums, 9);
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
    }

    internal static void ExecuteP009()
    {
        int x = 121;
        var item = new P009();
        var result = item.IsPalindrome(x);
        Console.WriteLine(result);
    }

    internal static void ExecuteP014()
    {
        string[] str = { "flower", "flow", "flight" };
        //string[] str = { "ab", "a" };
        var item = new P014();
        var result = item.LongestCommonPrefix(str);
        Console.WriteLine(result);
    }

    internal static void ExecuteP013()
    {
        string s = "MCMXCIV";
        //string[] str = { "ab", "a" };
        var item = new P013();
        var result = item.RomanToInt(s);
        Console.WriteLine(result);
    }
}