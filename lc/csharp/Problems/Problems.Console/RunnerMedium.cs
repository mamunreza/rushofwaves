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
        //var input = "aaba";
        //var input = "babad";
        //var input = "cbbd";
        var input = "cccbbdbbccf";
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

    internal static void ExecuteP167()
    {
        var item = new P167();

        //int[] nums = new int[] { 2, 7, 11, 15 };
        //var result = item.TwoSum(nums, 9);

        //int[] nums = new int[] { 2, 3, 4 };
        //var result = item.TwoSum(nums, 6);

        int[] nums = new int[] { -1, 0 };
        var result = item.TwoSum(nums, -1);

        Console.WriteLine(result[0]);
        Console.WriteLine(result[1]);
    }

    internal static void ExecuteP102()
    {
        var item = new P102();

        var n15 = new TreeNode(15);
        var n7 = new TreeNode(7);

        var n20 = new TreeNode(20);
        n20.left = n15;
        n20.right = n7;

        var n9 = new TreeNode(9);

        var n3 = new TreeNode(3);
        n3.left = n9;
        n3.right = n20;

        var result = item.LevelOrder(n3);
        Console.WriteLine(result);
    }

    internal static void ExecuteP133()
    {
        var item = new P133();

        var n1 = new Node(1);
        var n2 = new Node(2);
        var n3 = new Node(3);
        var n4 = new Node(4);

        n1.neighbors.Add(n2);
        n1.neighbors.Add(n4);

        n2.neighbors.Add(n1);
        n2.neighbors.Add(n3);

        n3.neighbors.Add(n2);
        n3.neighbors.Add(n4);

        n4.neighbors.Add(n1);
        n4.neighbors.Add(n3);

        var result = item.CloneGraph(n1);
        Console.WriteLine(result.val);
    }

    internal static void ExecuteP973() 
    {
        var item = new P973();

        var result = item.KClosest([[1, 3], [-2, 2]], 1);
        //var result = item.KClosest([[3, 3], [5, -1], [-2, 4]], 2);
    }
}
