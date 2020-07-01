using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeSourceGenerator.Common.Logging.Factories
{
    public class ConsoleLogOutputFactory : BaseLogOutputFactory, ILogOuputFactory
    {
        public ConsoleLogOutputFactory(string key, LogScope includedInScope) : base(key, includedInScope)
        {
        }

        public override bool IsInScope(LogModel model) => base.IsInScope(model);

        public override bool Log(LogModel model)
        {
            if (model != null)
            {
                Debug.WriteLine($"{GetScopeString(model.Scopes) ?? "Log"}: {model.Title} - {model.Message}");
                if (model.Exception != null)
                    Debug.WriteLine($"{model.Exception}");
                return base.Log(model);
            }
            else return false;
        }
    }
}
