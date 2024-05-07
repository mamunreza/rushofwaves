using System.Collections;

namespace Lib.Easy;

public class P205
{
    public bool IsIsomorphic(string s, string t)
    {
        if (s.Length != t.Length) return false;

        Hashtable s2t = new Hashtable();
        Hashtable t2s = new Hashtable();

        for (int i = 0; i < s.Length; i++)
        {
            var first = s[i];
            var second = t[i];

            if (s2t.ContainsKey(first) && (char)s2t[first] != t[i])
                return false;
            else if (t2s.ContainsKey(second) && (char)t2s[second] != s[i])
                return false;
            else
            {
                s2t[first] = second;
                t2s[second] = first;
            }
        }

        return true;
    }
}
