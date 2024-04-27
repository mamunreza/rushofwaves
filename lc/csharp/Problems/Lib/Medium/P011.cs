namespace Lib.Medium;

public class P011
{
    public int MaxArea(int[] height)
    {
        int maxArea = 0;
        int l = 0;
        int r = height.Length - 1;
        
        while(l < r)
        {
            var currentArea = Math.Min(height[l], height[r]) * (r - l);
            maxArea = Math.Max(maxArea, currentArea);

            if (height[l] < height[r])
            {
                l++;
            }
            else
            {
                r--;
            }
        }
        
        return maxArea;
    }
}
