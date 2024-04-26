namespace Lib.Medium;

public class P005
{
    public string LongestPalindrome(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            for (int l = i, r = i; l >= 0 && r < s.Length && s[l] == s[r]; l--, r++)
            {
                Console.WriteLine("l1 -> " + l + " - " + r);
            }

            for (int l = i, r = i + 1; l >= 0 && r < s.Length && s[l] == s[r]; l--, r++)
            {
                Console.WriteLine("l2 -> " + l + " - " + r);
            }
        }


        return string.Empty;
    }
}
