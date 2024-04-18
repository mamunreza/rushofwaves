using Lib.Easy;

namespace Problems.Cnsl;

internal class Program
{
    static void Main(string[] args)
    {
        ExecuteTwoSum();
    }

    private static void ExecuteTwoSum()
    {
        int[] nums = { 2, 7, 11, 15 };
        var p = new P001();
        var result = p.TwoSum(nums, 9);
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
    }
}
