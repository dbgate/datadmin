using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Plugin.textio
{
    public static class TextTool
    {
        public static IEnumerable<string> SplitBy(string input, string separator, bool isregex)
        {
            Regex linesepreg = null;
            if (isregex) linesepreg = new Regex(separator, RegexOptions.Compiled);

            int index = 0;
            while (index < input.Length)
            {
                int nextindex = -1;
                int matchlen = 0;
                if (isregex)
                {
                    var m = linesepreg.Match(input, index);
                    if (m.Success)
                    {
                        nextindex = m.Index;
                        matchlen = m.Length;
                    }
                    else
                    {
                        matchlen = 0;
                    }
                }
                else
                {
                    nextindex = input.IndexOf(separator, index);
                    if (nextindex < 0) matchlen = 0;
                    else matchlen = separator.Length;
                }
                if (matchlen == 0)
                {
                    yield return input.Substring(index);
                    yield break;
                }
                else
                {
                    yield return input.Substring(index, nextindex - index);
                    index = nextindex + matchlen;
                }
            }
        }

        public static string GetGroupValue(this Match m, string group)
        {
            if (!String.IsNullOrEmpty(group))
            {
                int val = 0;
                if (Int32.TryParse(group, out val))
                {
                    return m.Groups[val].Value;
                }
                else
                {
                    return m.Groups[group].Value;
                }
            }
            else
            {
                return m.Value;
            }
        }

        public static string ReplaceAll(this string value, Dictionary<string, string> vars)
        {
            if (vars == null) return value;
            foreach (string var in vars.Keys)
            {
                value = value.Replace(var, vars[var]);
            }
            return value;
        }
    }
}
