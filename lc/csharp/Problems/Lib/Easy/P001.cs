using System.Collections;

namespace Lib.Easy;

public class P001
{
    public int[] TwoSum(int[] nums, int target)
    {
        Hashtable ht = new();
        int[] result = new int[2];
        for (int i = 0; i < nums.Length; i++)
        {
            var diff = target - nums[i];
            if (ht.ContainsKey(diff))
            {
                result = [(int)ht[diff]!, i];
                break;
            }
            ht[nums[i]] = i;
        }
        return result;
    }
}
