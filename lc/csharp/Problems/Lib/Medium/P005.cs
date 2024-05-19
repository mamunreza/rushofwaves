namespace Lib.Medium;

public class P005
{
    public string LongestPalindrome(string s)
    {
        int resl = 0;
        int resr = 0;
        for (int i = 0; i < s.Length; i++)
        {
            for (int a = i, b = i; b <= i + 1; b++)
            {
                int l = a;
                int r = b;
                while (l >= 0 && r < s.Length && s[l] == s[r])
                {
                    if (r - l > resr - resl)
                    {
                        resl = l;
                        resr = r;
                    }
                    l--;
                    r++;
                }
            }
        }


        return s.Substring(resl, resr - resl + 1);
    }
}
