namespace Lib.Easy;

/// <summary>
/// 2 pointer
/// </summary>
public class P392
{
    public bool IsSubsequence(string s, string t)
    {
        if (s == null || s.Length == 0) return true;
        int sp = 0;
        int tp = 0;

        while (sp < s.Length && tp < t.Length)
        {
            if (s[sp] == t[tp]) sp++;
            tp++;
        }

        return sp == s.Length;
    }
}
