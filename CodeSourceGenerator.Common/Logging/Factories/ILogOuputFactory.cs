using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSourceGenerator.Common.Logging.Factories
{
    public interface ILogOuputFactory
    {
        string Key { get; }
        bool IsInScope(LogModel model);
        bool Log(LogModel model);
    }
}
