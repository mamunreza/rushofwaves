namespace Lib.Easy;

public class P058
{
    public int LengthOfLastWord(string s)
    {
        int length = 0;
        for (int i = s.Length-1; i >= 0; i--)
        {
            if (s[i] != ' ')
            {
                length++;
                continue;
            }

            if (length > 0)
                break;
        }

        return length;
    }
}
