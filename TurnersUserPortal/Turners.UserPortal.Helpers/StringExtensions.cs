using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Turners.UserPortal.Helpers
{
    public static class StringExtensions
    {
        public static string Sanitize(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Replace("*", "")
                    .Replace("/", "")
                    .Replace("\\", "")
                    .Replace("=", "")
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("+", "")
                    .Replace("-", "")
                    .Replace("&", "")
                    .Replace("|", "")
                    .Replace("!", "")
                    .Replace("$", "")
                    .Replace("%", "")
                    .Replace("^", "")
                    .Replace("#", "")
                    .Replace("@", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace("\"", "")
                    .Replace("'", "")
                    .Replace("?", "")
                    .Replace(",", "");
        }


        public static string[] SplitCSV(this string input)
        {
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"])*\"|[^,]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }
    }
}
