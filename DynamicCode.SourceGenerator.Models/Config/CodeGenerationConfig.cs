using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.Config
{
    public class CodeGenerationConfig
    {
        public CodeGenerationConfigSettings Settings { get; set; }
        public CodeGenerationConfigDebugging Debugging { get; set; }
        public List<CodeGenerationConfigBuilder> Builders { get; set; }
    }
}
