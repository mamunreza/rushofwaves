namespace Lib.Medium;

/// <summary>
/// 2 pointer
/// </summary>
public class P167
{
    public int[] TwoSum(int[] numbers, int target)
    {
        int l = 0;
        int r = numbers.Length - 1;
        int sum = 0;

        while (l < r)
        {
            sum = numbers[l] + numbers[r];
            if (sum == target) return [l + 1, r + 1];
            else if (sum < target) l += 1;
            else r -= 1;

        }

        return [-1, -1];
    }
}
