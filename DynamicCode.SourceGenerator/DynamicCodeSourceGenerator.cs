using DynamicCode.SourceGenerator.Models.Config;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DynamicCode.SourceGenerator
{
    [Generator]
    public class DynamicCodeSourceGenerator : ISourceGenerator
    {
        private CodeGenerationConfig _config;
        public void Initialize(InitializationContext context)
        {
            //Debugger.Launch();
            _config = ConfigParser.GetConfig<CodeGenerationConfig>();
        }

        public void Execute(SourceGeneratorContext context)
        {
            WriteGeneratedCode(context);
        }

        private void WriteGeneratedCode(SourceGeneratorContext context)
        {
            WriteGeneratedCode(context, "Test.cs");
        }

        private void WriteGeneratedCode(SourceGeneratorContext context, string fileName)
        {
            var sourceBuilder = new StringBuilder($@"");

            foreach (var item in _config.Builders)
            { 
                sourceBuilder.AppendLine(item.Template);

            }


            var source = SourceText.From(sourceBuilder.ToString(), Encoding.UTF8);
            context.AddSource(fileName, source);

            File.WriteAllText(fileName, source.ToString());
        }
    }
}
