using DynamicCode.SourceGenerator.Models.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicCode.SourceGenerator.Models.Generations
{
    public class RenderResultModel
    {
        public string Result { get; set; }
        public List<string> OutputPaths { get; set; } = new List<string>();
        public CodeGenerationConfigBuilder BuilderConfig { get; set; }

        public void AppendResultBefore(string result)
        {
            Result = result + Environment.NewLine + Environment.NewLine + Result;
        }
        public void AppendResultAfter(string result)
        {
            Result = Result + Environment.NewLine + Environment.NewLine + result;
        }
    }
}
