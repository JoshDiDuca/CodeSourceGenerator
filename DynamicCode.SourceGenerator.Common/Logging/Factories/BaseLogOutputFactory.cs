using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DynamicCode.SourceGenerator.Common.Logging.Factories
{
    public class BaseLogOutputFactory : ILogOuputFactory
    {
        public string Key { get; protected set; }
        public LogScope IncludedScope { get; protected set; }
        public string AdditionalKey { get; protected set; }

        public BaseLogOutputFactory(string key, LogScope includedScope, string additionalKey = null)
        {
            Key = key;
            IncludedScope = includedScope;
            AdditionalKey = additionalKey;
        }

        public virtual bool IsInScope(LogModel model) => true;

        public virtual bool Log(LogModel model)
        {
            return true;
        }

        protected string GetScopeString(LogScope scope)
        {
            StringBuilder builder = new StringBuilder();
            var scopeStrings = Enum.GetValues(typeof(LogScope)).Cast<LogScope>()?.Where(s => scope.HasFlag(s))?.Select(s => s.ToString());
            if (scopeStrings != null)
                return string.Join(", ", scopeStrings);
            else return null;
        }
    }
}
