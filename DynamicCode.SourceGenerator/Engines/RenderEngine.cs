using DynamicCode.SourceGenerator.Functions;
using DynamicCode.SourceGenerator.Metadata.Interfaces;
using DynamicCode.SourceGenerator.Models.Config;
using DynamicCode.SourceGenerator.Models.Generations;
using DynamicCode.SourceGenerator.Models.Rendering;
using DynamicCode.SourceGenerator.Visitors;
using Microsoft.CodeAnalysis;
using Scriban;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DynamicCode.SourceGenerator.Engines
{
    public class RenderEngine
    {
        public List<INamedItem> GetMatchedObjects(SourceFileSymbolVisitor visitor, SourceGeneratorContext context, CodeGenerationConfigBuilder builder)
        {
            visitor.Visit(context.Compilation.GlobalNamespace);

            builder.Input.Assemblies ??= new List<string>();

            if (!builder.Input.Assemblies.Any())
                builder.Input.Assemblies.Add(context.Compilation.Assembly.Name);

            List<INamedItem> queryObjects = new List<INamedItem>();

            if (!string.IsNullOrEmpty(builder.Input.InputMatcher))
            {
                var matchedObjects = visitor.QueryObjects(builder.Input.InputMatcher, builder.Input.Assemblies);
                if (matchedObjects != null)
                    queryObjects.AddRange(matchedObjects);
            }

            if (builder.Input.InputMatchers != null && builder.Input.InputMatchers.Any())
            {
                foreach (var matcher in builder.Input.InputMatchers)
                {
                    var matchedObjects = visitor.QueryObjects(matcher, builder.Input.Assemblies);
                    if (matchedObjects != null)
                        queryObjects.AddRange(matchedObjects);
                }
            }

            if (builder.Input.InputIgnoreMatchers != null && builder.Input.InputIgnoreMatchers.Any())
            {
                foreach (var matcher in builder.Input.InputIgnoreMatchers)
                {
                    var matchedObjects = visitor.QueryObjects(matcher, builder.Input.Assemblies);
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

        public RenderResultModel RenderMatch(INamedItem @object, CodeGenerationConfigBuilder builder)
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

            var renderResult = new RenderResultModel { Result = result, BuilderConfig = builder, OutputPaths = fileNames };

            return renderResult;
        }
    }
}
