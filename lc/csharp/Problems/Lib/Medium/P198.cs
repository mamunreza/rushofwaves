namespace Lib.Medium;

public class P198
{
    public int Rob(int[] nums)
    {
        int[] cache = new int[nums.Length];
        for (int i = 0; i < cache.Length; i++)
        {
            cache[i] = -1;
        }
        var result = RobHouse(0, nums, cache);
        return result;
    }

    static int RobHouse(int i, int[] nums, int[] cache)
    {
        if (i >= nums.Length)
        {
            return 0;
        }

        if (cache[i] >= 0)
        {
            return cache[i];
        }

        var r1 = nums[i] + RobHouse(i + 2, nums, cache);
        var r2 = RobHouse(i + 1, nums, cache);
        Console.WriteLine(i);
        cache[i] = Math.Max(r1, r2);
        return cache[i];
    }

}
