using System.Collections;

namespace Lib.Easy;

public class P290
{
    public bool WordPattern(string pattern, string s)
    {
        var words = s.Split(' ');

        if (pattern.Length != words.Length) return false;

        Hashtable c2w = [];
        Hashtable w2c = [];

        for (int i = 0; i < pattern.Length; i++)
        {
            var c = pattern[i];
            var word = words[i];

            if (c2w.ContainsKey(c) && c2w[c]?.ToString() != word)
            {
                return false;
            }
            else if(w2c.ContainsKey(word) && (char)w2c[word] != c)
            {
                return false;
            }
            else
            {
                c2w[c] = word;
                w2c[word] = c;
            }
        }

        return true;
    }
}
