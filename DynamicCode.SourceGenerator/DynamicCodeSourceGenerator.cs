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
using DynamicCode.SourceGenerator.Common;

namespace DynamicCode.SourceGenerator
{
    [Generator]
    public class DynamicCodeSourceGenerator : ISourceGenerator
    {
        private CodeGenerationConfig _config;
        private SourceFileSymbolVisitor _visitor;
        private int currentGeneration = 0;

        private Dictionary<string, List<GenerationModel<string>>> _generations;
        public List<KeyValuePair<string, string>> CurrentGenerations => GetGeneration(currentGeneration);
        public List<KeyValuePair<string, string>> PreviousGenerations => GetGeneration(currentGeneration - 1);

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

            if (_config?.Builders == null)
                return;

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

                foreach (INamedItem @object in GetMatchedObjects(builder, assemblies))
                {
                    var scriptObject = new ScriptObject();
                    scriptObject.Import(typeof(StringFunctions));

                    var renderModel = RenderModel.FromNamedItem(builder, @object);
                    scriptObject.Import(renderModel);

                    var templateContext = new TemplateContext();
                    templateContext.PushGlobal(scriptObject);

                    var template = Template.Parse(File.ReadAllText(builder.Template));
                    var fileNameTemplate = Template.Parse(builder.OutputName);

                    var result = template.Render(templateContext);
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
            }

            foreach (var pair in PreviousGenerations)
            {
                if (File.Exists(pair.Key))
                {
                    File.Delete(pair.Key);
                }
            }

            foreach (var pair in CurrentGenerations)
            {
                try
                {
                    var source = SourceText.From(pair.Value, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(pair.Key))
                    {
                        context.AddSource(Path.GetFileName(pair.Key), source);
                        if (!Directory.Exists(Path.GetDirectoryName(pair.Key)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(pair.Key));
                        }
                        if (File.Exists(pair.Key))
                        {
                            File.Delete(pair.Key);
                        }
                        File.WriteAllText(pair.Key, source.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error rendering templates", $"Could not render template output {pair.Key}", ex);


                }
            }
        }

        private List<INamedItem> GetMatchedObjects(CodeGenerationConfigBuilder builder, List<string> assemblies)
        {
            List<INamedItem> queryObjects = new List<INamedItem>();

            if (!string.IsNullOrEmpty(builder.InputMatcher))
            {
                var matchedObjects = _visitor.QueryObjects(builder.InputMatcher, assemblies);
                if (matchedObjects != null)
                    queryObjects.AddRange(matchedObjects);
            }

            if (builder.InputMatchers != null && builder.InputMatchers.Any())
            {
                foreach (var matcher in builder.InputMatchers)
                {
                    var matchedObjects = _visitor.QueryObjects(matcher, assemblies);
                    if (matchedObjects != null)
                        queryObjects.AddRange(matchedObjects);
                }
            }

            if (builder.InputIgnoreMatchers != null && builder.InputIgnoreMatchers.Any())
            {
                foreach (var matcher in builder.InputIgnoreMatchers)
                {
                    var matchedObjects = _visitor.QueryObjects(matcher, assemblies);
                    if (matchedObjects != null)
                    {
                        foreach (var match in matchedObjects)
                        {
                            queryObjects = queryObjects.Where(o => o.FullName != match.FullName).ToList();
                        }
                    }
                }
            }

            return queryObjects;
        }

        private List<KeyValuePair<string, string>> GetGeneration(int generation)
            => _generations?.Where(p => p.Value.Any(g => g.Generation == generation))?.Select(g => new KeyValuePair<string, string>(g.Key, g.Value.FirstOrDefault(g => g.Generation == generation).Model))?.ToList();
    }
}
