using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DynamicCode.SourceGenerator.Functions
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
    }
}
