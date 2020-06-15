using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.Config
{
    public class CodeGenerationConfigBuilder
    {
        public string Template { get; set; }
        public string OutputName { get; set; }
        public string ObjectNames { get; set; }
        public List<string> Assemblies { get; set; }

    }
}