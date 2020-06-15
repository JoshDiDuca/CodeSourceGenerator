using DynamicCode.SourceGenerator.Models;
using DynamicCode.SourceGenerator.Models.Config;
using DynamicCode.SourceGenerator.Visitors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Scriban;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DynamicCode.SourceGenerator
{
    [Generator]
    public class DynamicCodeSourceGenerator : ISourceGenerator
    {
        private CodeGenerationConfig _config;
        private SourceFileSymbolVisitor _visitor;
        public void Initialize(InitializationContext context)
        {
            Debugger.Launch();
            _config = ConfigParser.GetConfig<CodeGenerationConfig>();
            _visitor = new SourceFileSymbolVisitor();
        }

        public void Execute(SourceGeneratorContext context)
        {
            _visitor.Visit(context.Compilation.GlobalNamespace);
            WriteGeneratedCode(context);
        }

        private void WriteGeneratedCode(SourceGeneratorContext context)
        {
            foreach (CodeGenerationConfigBuilder builder in _config.Builders)
            {
                List<string> assemblies = builder.Assemblies ?? new List<string>();
                assemblies.Add(context.Compilation.AssemblyName);

                foreach (BaseObject @object in _visitor.QueryObjects(builder.ObjectNames, assemblies))
                {
                    var template = Template.Parse(File.ReadAllText(builder.Template));
                    var fileNameTemplate = Template.Parse(builder.OutputName);
                    var renderModel = new { Template = builder.Template, Name = @object.Name, Members = @object.Members };

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
