using System;

namespace DynamicCode.SourceGenerator.Common.Logging
{
    public class LogModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public LogScope Scopes { get; set; }
        public string AdditionalKey { get; set; }
    }

    [Flags]
    public enum LogScope 
    {
        Information = 0,
        Warning = 1,
        Error = 2
    };
}
