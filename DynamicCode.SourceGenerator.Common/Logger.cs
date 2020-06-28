using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DynamicCode.SourceGenerator.Common
{
    public class Logger
    {
        public static void LogError(string title, string message, Exception ex)
        {
            Debug.WriteLine($"ERROR: {title} - {message}");
            Debug.WriteLine($"{ex}");
        }
    }
}
