using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeSourceGenerator.Common.Logging.Factories
{
    public class BaseLogOutputFactory : ILogOuputFactory
    {
        public string Key { get; protected set; }
        public LogScope? IncludedScope { get; protected set; }
        public LogScope? ExcludeScope { get; protected set; }
        public string AdditionalKey { get; protected set; }

        public BaseLogOutputFactory(string key, LogScope? includedScope, LogScope? excludeScope = null, string additionalKey = null)
        {
            Key = key;
            IncludedScope = includedScope;
            ExcludeScope = excludeScope;
            AdditionalKey = additionalKey;
        }

        public virtual bool IsInScope(LogModel model) => IncludedScope?.HasFlag(model.Scopes) == true && (ExcludeScope != null && ExcludeScope.Value.HasFlag(model.Scopes));

        public virtual bool Log(LogModel model)
        {
            return true;
        }

        protected string GetScopeString(LogScope scope)
        {
            StringBuilder builder = new StringBuilder();
            var scopeStrings = Enum.GetValues(typeof(LogScope)).Cast<LogScope>()?.Where(s => s == scope);
            if (scopeStrings != null)
                return string.Join(", ", scopeStrings);
            else return null;
        }
    }
}
