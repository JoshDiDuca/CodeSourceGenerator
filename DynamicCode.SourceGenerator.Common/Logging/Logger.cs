using DynamicCode.SourceGenerator.Common.Logging.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DynamicCode.SourceGenerator.Common.Logging
{
    public class Logger
    {
        public List<ILogOuputFactory> OutputFactories { get; protected set; } = new List<ILogOuputFactory>();

        public void RegisterFactory(ILogOuputFactory factory, bool removeOfKey = true)
        {
            if (factory != null)
            {
                if (removeOfKey)
                {
                    ILogOuputFactory factoryOfKey = OutputFactories.FirstOrDefault(f => f.Key == factory.Key && f.GetType() != factory.GetType());

                    if (factoryOfKey != null)
                        OutputFactories.Remove(factoryOfKey);
                }

                OutputFactories.Add(factory);
            }
        }

        public void LogError(string title, string message, LogScope scopes, Exception ex = null)
        {
            LogError(new LogModel { Title = title, Message = message, Exception = ex });
        }
        public void LogError(string title, string message, Exception ex = null)
        {
            LogError(new LogModel { Title = title, Message = message, Exception = ex, Scopes = LogScope.Error });
        }

        public void LogError(LogModel model)
        {
            foreach (ILogOuputFactory factory in OutputFactories.Where(o => o.IsInScope(model)))
                factory.Log(model);
        }
    }
}
