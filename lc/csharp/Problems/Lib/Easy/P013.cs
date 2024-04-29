namespace Lib.Easy;

public class P013
{
    public int RomanToInt(string s)
    {
        Dictionary<char, int> mapping = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        int prev = 0;
        int result = 0;
        foreach(var c in s.Reverse())
        {
            if (mapping[c] < prev)
                result -= mapping[c];
            else
                result += mapping[c];

            prev = mapping[c];
        }

        return result;
    }
}
