using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DynamicCode.SourceGenerator.Common
{
    public class Logger
    {
        public static void LogError(string title, string message, Exception ex = null)
        {
            Debug.WriteLine($"ERROR: {title} - {message}");
            if(ex != null)
                Debug.WriteLine($"{ex}");
        }
    }
}
