namespace Lib.Medium;

public class P053
{
    public int MaxSubArray(int[] nums)
    {
        int maxSum = nums[0];
        int currentsum = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (currentsum < 0)
                currentsum = 0;
            currentsum += nums[i];
            maxSum = Math.Max(maxSum, currentsum);
        }

        return maxSum;
    }
}
