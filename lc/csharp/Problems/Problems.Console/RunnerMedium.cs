using Lib.Medium;

namespace Problems.Cnsl;

internal class RunnerMedium
{

    internal static void ExecuteP003()
    {
        //var s = "abcabcbb";
        //var s = "bbbbb";
        var s = "pwwkew";
        //var s = "aabaab!bb";
        var item = new P003();
        var result = item.LengthOfLongestSubstring(s);
        Console.WriteLine(result);
    }

    internal static void ExecuteP005()
    {
        var item = new P005();
        var input = "aaba";
        var result = item.LongestPalindrome(input);
        Console.WriteLine(result);
    }

    internal static void ExecuteP011()
    {
        var item = new P011();
        //int[] input = new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
        int[] input = new int[] { 1, 1 };
        var result = item.MaxArea(input);
        Console.WriteLine(result);
    }

    internal static void ExecuteP053()
    {
        var item = new P053();
        //int[] nums = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
        //int[] nums = new int[] { 1 };
        int[] nums = new int[] { 5, 4, -1, 7, 8 };
        var result = item.MaxSubArray(nums);
        Console.WriteLine(result);
    }

    internal static void ExecuteP198()
    {
        var item = new P198();
        //int[] nums = new int[] { 1, 2, 3, 1 };
        //int[] nums = new int[] { 2, 7, 9, 3, 1 };
        int[] nums = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var result = item.Rob(nums);
        Console.WriteLine(result);
    }
}
