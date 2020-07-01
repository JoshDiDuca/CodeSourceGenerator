using CodeSourceGenerator.Common;
using CodeSourceGenerator.Common.Logging;
using CodeSourceGenerator.Functions;
using CodeSourceGenerator.Helpers;
using CodeSourceGenerator.Metadata.Interfaces;
using CodeSourceGenerator.Models.Config;
using CodeSourceGenerator.Models.Generations;
using CodeSourceGenerator.Models.Rendering;
using CodeSourceGenerator.Visitors;
using Microsoft.CodeAnalysis;
using Scriban;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSourceGenerator.Engines
{
    public class RenderEngine
    {
        private Logger _logger;
        public RenderEngine(Logger logger)
        {
            _logger = logger;
        }

        public List<INamedItem> GetMatchedObjects(SourceGeneratorContext context, SourceFileSymbolVisitor visitor, CodeGenerationConfigBuilder builder)
        {
            visitor.Visit(context.Compilation.GlobalNamespace);

            builder.Input.Assemblies ??= new List<string>();

            if (!builder.Input.Assemblies.Any())
                builder.Input.Assemblies.Add(context.Compilation.Assembly.Name);

            List<INamedItem> queryObjects = new List<INamedItem>();

            if (!string.IsNullOrEmpty(builder.Input.InputMatcher))
            {
                List<INamedItem> matchedObjects = visitor.QueryObjects(builder.Input.InputMatcher, builder.Input.Assemblies);
                if (matchedObjects != null)
                    queryObjects.AddRange(matchedObjects);
            }

            if (builder.Input.InputMatchers != null && builder.Input.InputMatchers.Any())
            {
                foreach (string matcher in builder.Input.InputMatchers)
                {
                    List<INamedItem> matchedObjects = visitor.QueryObjects(matcher, builder.Input.Assemblies);
                    if (matchedObjects != null)
                        queryObjects.AddRange(matchedObjects);
                }
            }

            if (builder.Input.InputIgnoreMatchers != null && builder.Input.InputIgnoreMatchers.Any())
            {
                foreach (string matcher in builder.Input.InputIgnoreMatchers)
                {
                    List<INamedItem> matchedObjects = visitor.QueryObjects(matcher, builder.Input.Assemblies);
                    if (matchedObjects != null)
                    {
                        foreach (INamedItem match in matchedObjects)
                        {
                            queryObjects = queryObjects.Where(o => o.FullName != match.FullName).ToList();
                        }
                    }
                }
            }

            return queryObjects;
        }

        public RenderResultModel RenderMatch(INamedItem @object, CodeGenerationConfigBuilder builder)
        {

            Models.RenderModels.Object renderModel = RenderModel.FromNamedItem(builder, @object);

            var serialRenderModel = JSON.Convert(renderModel);
            _logger.LogError(new LogModel
            {
                Message = serialRenderModel,
                Title = renderModel?.ToString(),
                AdditionalKey = $"{renderModel?.ToString()}_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}",
                Scopes = LogScope.Objects
            });


            TemplateContext templateContext = new TemplateContext();
            ScriptObject scriptObject = new ScriptObject();
            scriptObject.Import(typeof(StringFunctions));
            scriptObject.Import(renderModel);
            templateContext.PushGlobal(scriptObject);

            string templateContent = TemplateHelper.FindTemplateFile(builder.Input.Template);

            if (!string.IsNullOrEmpty(templateContent))
            {
                Template template = Template.Parse(templateContent);

                List<Template> fileNameTemplates = new List<Template>();
                if (builder.Output.OutputPathTemplates != null && builder.Output.OutputPathTemplates.Any())
                    fileNameTemplates.AddRange(builder.Output.OutputPathTemplates.Select(t => Template.Parse(t)));
                else
                    fileNameTemplates.Add(Template.Parse(builder.Output.OutputPathTemplate));

                string result = template.Render(templateContext);

                List<string> outputFilePaths = new List<string>(fileNameTemplates.Select(r => r.Render(templateContext)));

                RenderResultModel renderResult = new RenderResultModel { Result = result, BuilderConfig = builder, OutputPaths = outputFilePaths };

                return renderResult;
            } 
            else
            {
                _logger.LogError("Error finding template", "Could not find the following template: "+ builder.Input.Template);
                return null;
            }
        }
    }
}
