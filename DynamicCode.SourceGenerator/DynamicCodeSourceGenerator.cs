using DynamicCode.SourceGenerator.Models.Config;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Scriban;
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
            foreach (var builder in _config.Builders)
            {
                foreach (var file in Directory.GetFiles(Path.GetDirectoryName(builder.InputPath), Path.GetFileName(builder.InputPath)))
                {
                    var template = Template.Parse(File.ReadAllText(builder.Template));
                    var fileNameTemplate = Template.Parse(builder.OutputName);
                    var renderModel = new { Template = builder.Template, FilePath = file, FileName = Path.GetFileName(file), FileDirectory = Path.GetDirectoryName(file) };

                    var result = template.Render(renderModel);
                    var fileName = fileNameTemplate.Render(renderModel);

                    var source = SourceText.From(result, Encoding.UTF8);

                    context.AddSource(fileName, source);
                    File.WriteAllText(fileName, source.ToString());
                }
            }
        }
    }
}
