namespace Lib.Easy;

public class P027
{
    public int RemoveElement(int[] nums, int val)
    {
        int next = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != val)
            {
                nums[next] = nums[i];
                next++;
            }
        }
        return next;
    }
}