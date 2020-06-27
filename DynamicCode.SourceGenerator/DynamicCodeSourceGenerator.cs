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

        private Dictionary<string, List<GenerationModel<RenderResultModel>>> _generations;
        public List<KeyValuePair<string, RenderResultModel>> CurrentGenerations => GetGeneration(currentGeneration);
        public List<KeyValuePair<string, RenderResultModel>> PreviousGenerations => GetGeneration(currentGeneration - 1);

        public void Initialize(InitializationContext context)
        {
            Debugger.Launch();
            currentGeneration = 0;
            _generations = new Dictionary<string, List<GenerationModel<RenderResultModel>>>();
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
                if(builder.Input == null || builder.Output == null)
                {
                    Logger.LogError("Skipping generation", "Input or ouput object is not provided.", null);
                    continue;
                }

                List<string> assemblies = builder.Input.Assemblies ?? new List<string>();
                assemblies.Add(context.Compilation.Assembly.Name);

                foreach (INamedItem @object in GetMatchedObjects(builder, assemblies))
                {
                    var scriptObject = new ScriptObject();
                    scriptObject.Import(typeof(StringFunctions));

                    var renderModel = RenderModel.FromNamedItem(builder, @object);
                    scriptObject.Import(renderModel);

                    var templateContext = new TemplateContext();
                    templateContext.PushGlobal(scriptObject);

                    var template = Template.Parse(File.ReadAllText(builder.Input.Template));

                    var fileNameTemplates = new List<Template>();

                    if (builder.Output.OutputPathTemplates != null && builder.Output.OutputPathTemplates.Any())
                    {
                        fileNameTemplates.AddRange(builder.Output.OutputPathTemplates.Select(t => Template.Parse(t)));
                    }
                    else
                    {
                        fileNameTemplates.Add(Template.Parse(builder.Output.OutputPathTemplate));
                    }


                    var result = template.Render(templateContext);

                    var fileNames = new List<string>();

                    foreach (var fileNameTemplate in fileNameTemplates)
                    {
                        fileNames.Add(fileNameTemplate.Render(renderModel));
                    }

                    var renderResult = new RenderResultModel { Result = result, BuilderConfig = builder };

                    var newGeneration = new GenerationModel<RenderResultModel>(currentGeneration, renderResult);

                    foreach (var fileName in fileNames)
                    {
                        var previousGens = _generations.ContainsKey(fileName) ? _generations[fileName] : null;

                        if (previousGens is null || !previousGens.Any())
                        {
                            _generations.Add(fileName, new List<GenerationModel<RenderResultModel>> { newGeneration });
                        }
                        else
                        {
                            var currentGen = previousGens.FirstOrDefault(g => g.Generation == currentGeneration);
                            if (currentGen == null)
                            {
                                previousGens.Add(newGeneration);
                            }
                            else
                            {
                                renderResult = new RenderResultModel { Result = currentGen.Model.Result + Environment.NewLine + Environment.NewLine + result, BuilderConfig = builder };
                                currentGen.Model = renderResult;
                            }
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
                    var source = SourceText.From(pair.Value?.Result, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(pair.Key))
                    {
                        if (pair.Value.BuilderConfig.Output.AddToCompilation)
                        {
                            context.AddSource(Path.GetFileName(pair.Key), source);
                        }

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

            if (!string.IsNullOrEmpty(builder.Input.InputMatcher))
            {
                var matchedObjects = _visitor.QueryObjects(builder.Input.InputMatcher, assemblies);
                if (matchedObjects != null)
                    queryObjects.AddRange(matchedObjects);
            }

            if (builder.Input.InputMatchers != null && builder.Input.InputMatchers.Any())
            {
                foreach (var matcher in builder.Input.InputMatchers)
                {
                    var matchedObjects = _visitor.QueryObjects(matcher, assemblies);
                    if (matchedObjects != null)
                        queryObjects.AddRange(matchedObjects);
                }
            }

            if (builder.Input.InputIgnoreMatchers != null && builder.Input.InputIgnoreMatchers.Any())
            {
                foreach (var matcher in builder.Input.InputIgnoreMatchers)
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

        private List<KeyValuePair<string, RenderResultModel>> GetGeneration(int generation)
            => _generations?.Where(p => p.Value.Any(g => g.Generation == generation))?.Select(g => new KeyValuePair<string, RenderResultModel>(g.Key, g.Value.FirstOrDefault(g => g.Generation == generation).Model))?.ToList();
    }
}
