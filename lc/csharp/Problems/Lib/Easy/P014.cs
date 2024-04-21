namespace Lib.Easy;

public class P014
{
    public string LongestCommonPrefix(string[] strs)
    {
        var resultLen = 0;
        var misMatchFound = false;
        for (int i = 0; i < strs[0].Length; i++)
        {
            var current = strs[0][i];
            foreach (var item in strs)
            {
                if (item.Length <= i || item[i] != current)
                {
                    misMatchFound = true;
                    break;
                }
            }

            if (misMatchFound)
                break;
            else
                resultLen++;
        }

        return strs[0].Substring(0, resultLen);
    }
}
