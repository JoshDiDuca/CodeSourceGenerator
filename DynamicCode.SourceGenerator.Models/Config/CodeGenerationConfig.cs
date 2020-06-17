using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicCode.SourceGenerator.Models.Config
{
    public class CodeGenerationConfig
    {
        public CodeGenerationConfigSettings Settings { get; set; }
        public List<CodeGenerationConfigBuilder> Builders { get; set; }
    }
}
