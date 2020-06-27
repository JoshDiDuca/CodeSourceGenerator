using DynamicCode.SourceGenerator.Models.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicCode.SourceGenerator.Models.Generations
{
    public class RenderResultModel
    {
        public string Result { get; set; }
        public CodeGenerationConfigBuilder BuilderConfig { get; set; }
    }
}
