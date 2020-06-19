using DynamicCode.SourceGenerator.Common;
using DynamicCode.SourceGenerator.Metadata.Interfaces;
using DynamicCode.SourceGenerator.Models.Config;
using DynamicCode.SourceGenerator.Visitors;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DynamicCode.SourceGenerator
{
    public class ConfigParser
    {
        public static CodeGenerationConfig GetConfig(SourceFileSymbolVisitor visitor, SourceGeneratorContext context)
        {
            var file = File.ReadAllText("codegen.json");
            var config = JSON.Parse<CodeGenerationConfig>(file);
            if(config != null)
            {
                
            }
            return config;
        }
    }
}
