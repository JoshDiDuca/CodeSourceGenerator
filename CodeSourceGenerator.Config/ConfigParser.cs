using CodeSourceGenerator.Common;
using CodeSourceGenerator.Metadata.Interfaces;
using CodeSourceGenerator.Models.Config;
using CodeSourceGenerator.Visitors;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeSourceGenerator
{
    public class ConfigParser
    {
        public static CodeGenerationConfig GetConfig(SourceFileSymbolVisitor visitor, SourceGeneratorContext context)
        {
            string file = File.ReadAllText("codegen.json");
            CodeGenerationConfig config = JSON.Parse<CodeGenerationConfig>(file);
            if(config != null)
            {
                
            }
            return config;
        }
    }
}
