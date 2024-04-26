namespace Lib.Medium;

public class P003
{
    public int LengthOfLongestSubstring(string s)
    {
        int length = 0;
        HashSet<char> subString = new HashSet<char>();
        int l = 0;

        for (int r = 0; r < s.Length; r++)
        {
            while (subString.Contains(s[r]))
            {
                subString.Remove(s[l]);
                l += 1;
            }

            subString.Add(s[r]);
            length = Math.Max(length, r - l + 1);
        }

        return length;
    }
}
