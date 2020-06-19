using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DynamicCode.SourceGenerator.Functions
{
    public class StringFunctions
    {
        public string ToCamelCase(string s)
        {
            var x = s.Replace("_", "");
            if (x.Length == 0) return string.Empty;
            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToUpper(x[0]) + x.Substring(1);
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
