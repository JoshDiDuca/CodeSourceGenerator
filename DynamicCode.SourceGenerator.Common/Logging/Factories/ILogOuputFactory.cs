using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicCode.SourceGenerator.Common.Logging.Factories
{
    public interface ILogOuputFactory
    {
        string Key { get; }
        bool IsInScope(LogModel model);
        bool Log(LogModel model);
    }
}
