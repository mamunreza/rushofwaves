namespace Lib.Medium;

public class P005
{
    public string LongestPalindrome(string s)
    {
        int resl = 0;
        int resr = 0;
        for (int i = 0; i < s.Length; i++)
        {
            for (int l = i, r = i + 1; l < s.Length; l++, r++)
            {
                while (l >= 0 && r < s.Length && s[l] == s[r])
                {
                    if (r - l > resr - resl)
                    {
                        resl = l;
                        resr = r;
                    }
                    l--;
                    r++;
                    //Console.WriteLine("l: " + l + ", r: " + r);
                }
            }
        }


        return s.Substring(resl+1, resr+1);
    }
}
