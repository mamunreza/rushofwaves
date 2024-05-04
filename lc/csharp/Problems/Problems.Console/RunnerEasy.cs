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

    internal static void ExecuteP021()
    {
        var node11 = new ListNode
        {
            val = 1,
        };
        var node12 = new ListNode
        {
            val = 2,
        };
        var node13 = new ListNode
        {
            val = 4,
        };
        node11.next = node12;
        node12.next = node13;

        var node21 = new ListNode
        {
            val = 1,
        };
        var node22 = new ListNode
        {
            val = 3,
        };
        var node23 = new ListNode
        {
            val = 4,
        };
        node21.next = node22;
        node22.next = node23;

        var item = new P021();
        var result = item.MergeTwoLists(node11, node21);
        Console.WriteLine(result.val);
    }

    internal static void ExecuteP027()
    {
        int[] nums = { 3, 2, 2, 3 };
        //int[] nums = { 3, 2, 2, 3 }
        var item = new P027();
        var result = item.RemoveElement(nums, 3);
        Console.WriteLine(result);
    }

    internal static void ExecuteP028()
    {
        //string haystack = "sadbutsad";
        //string needle = "sad";
        string haystack = "leetcode";
        string needle = "leeto";

        var item = new P028();
        var result = item.StrStr(haystack, needle);
        Console.WriteLine(result);
    }

    internal static void ExecuteP058()
    {
        //string s = "Hello World";
        //string s = "   fly me   to   the moon  ";
        string s = "luffy is still joyboy";
        var item = new P058();
        var result = item.LengthOfLastWord(s);
        Console.WriteLine(result);
    }
}