using DynamicCode.SourceGenerator.Models.Config;
using DynamicCode.SourceGenerator.Models.Generations;
using DynamicCode.SourceGenerator.Visitors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DynamicCode.SourceGenerator.Metadata.Interfaces;
using DynamicCode.SourceGenerator.Models.Rendering;
using System.Diagnostics;
using Scriban.Runtime;
using DynamicCode.SourceGenerator.Functions;

namespace DynamicCode.SourceGenerator
{
    [Generator]
    public class DynamicCodeSourceGenerator : ISourceGenerator
    {
        private CodeGenerationConfig _config;
        private SourceFileSymbolVisitor _visitor;
        private int currentGeneration = 0;

        private Dictionary<string, List<GenerationModel<string>>> _generations;
        public List<KeyValuePair<string, string>> CurrentGenerations => _generations?.Where(p => p.Value.Any(g => g.Generation == currentGeneration))?.Select(g => new KeyValuePair<string, string>(g.Key, g.Value.FirstOrDefault(g => g.Generation == currentGeneration).Model))?.ToList();

        public void Initialize(InitializationContext context)
        {
            Debugger.Launch();
            currentGeneration = 0;
            _generations = new Dictionary<string, List<GenerationModel<string>>>();
            _visitor = new SourceFileSymbolVisitor();
        }

        public void Execute(SourceGeneratorContext context)
        {
            _config = ConfigParser.GetConfig(_visitor, context);
            
            currentGeneration++;
            _visitor.Visit(context.Compilation.GlobalNamespace);
            WriteGeneratedCode(context);
        }

        private void WriteGeneratedCode(SourceGeneratorContext context)
        {
            foreach (CodeGenerationConfigBuilder builder in _config.Builders)
            {
                List<string> assemblies = builder.Assemblies ?? new List<string>();
                assemblies.Add(context.Compilation.Assembly.Name);

                var queryObjects = _visitor.QueryObjects(builder.ObjectNames, assemblies);

                foreach (INamedItem @object in queryObjects)
                {
                    var scriptObject = new ScriptObject();
                    scriptObject.Import(typeof(StringFunctions));

                    var renderModel = RenderModel.FromNamedItem(builder, @object);

                    var templateContext = new TemplateContext(ScriptObject.From(renderModel));
                    templateContext.PushGlobal(scriptObject);

                    var template = Template.Parse(File.ReadAllText(builder.Template));
                    var fileNameTemplate = Template.Parse(builder.OutputName);

                    var result = template.Render(renderModel);
                    var fileName = fileNameTemplate.Render(renderModel);

                    var previousGens = _generations.ContainsKey(fileName) ? _generations[fileName] : null;

                    var newGeneration = new GenerationModel<string>(currentGeneration, result);
                    if (previousGens is null || !previousGens.Any())
                    {
                        _generations.Add(fileName, new List<GenerationModel<string>>() { newGeneration });
                    } 
                    else
                    {
                        var currentGen = previousGens.FirstOrDefault(g => g.Generation == currentGeneration);
                        if(currentGen == null)
                        {
                            previousGens.Add(newGeneration);
                        } 
                        else
                        {
                            result = currentGen.Model + Environment.NewLine + Environment.NewLine + result;
                            currentGen.Model = result;
                        }
                    }

                }

                foreach (var pair in CurrentGenerations)
                {
                    var source = SourceText.From(pair.Value, Encoding.UTF8);

                    context.AddSource(pair.Key, source);
                    File.WriteAllText(pair.Key, source.ToString());
                }
            }
        }
    }
}
