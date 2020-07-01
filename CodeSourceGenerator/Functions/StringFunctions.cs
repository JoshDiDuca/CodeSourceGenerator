using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeSourceGenerator.Functions
{
    public class StringFunctions : ScriptObject
    {
        public static string ToCamelCase(string s)
        {
            return string.IsNullOrEmpty(s) || s.Length < 2
            ? s
            : Char.ToLowerInvariant(s[0]) + s.Substring(1);
        }

        public static string PascalCase(string s)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
            s = Regex.Replace(s, "([A-Z]+)", " $1");
            s = cultInfo.ToTitleCase(s);
            s = s.Replace(" ", "");
            return s;
        }


        public static string ToKebabCase(string source)
        {
            if (source is null) return null;

            if (source.Length == 0) return string.Empty;

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < source.Length; i++)
            {
                if (char.IsLower(source[i])) // if current char is already lowercase
                {
                    builder.Append(source[i]);
                }
                else if (i == 0) // if current char is the first char
                {
                    builder.Append(char.ToLower(source[i]));
                }
                else if (char.IsLower(source[i - 1])) // if current char is upper and previous char is lower
                {
                    builder.Append("-");
                    builder.Append(char.ToLower(source[i]));
                }
                else if (i + 1 == source.Length || char.IsUpper(source[i + 1])) // if current char is upper and next char doesn't exist or is upper
                {
                    builder.Append(char.ToLower(source[i]));
                }
                else // if current char is upper and next char is lower
                {
                    builder.Append("-");
                    builder.Append(char.ToLower(source[i]));
                }
            }
            return builder.ToString();
        }
    }
}
