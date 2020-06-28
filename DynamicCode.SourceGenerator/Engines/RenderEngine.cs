using DynamicCode.SourceGenerator.Common;
using DynamicCode.SourceGenerator.Functions;
using DynamicCode.SourceGenerator.Helpers;
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
using System.Threading.Tasks;

namespace DynamicCode.SourceGenerator.Engines
{
    public class RenderEngine
    {
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
            ScriptObject scriptObject = new ScriptObject();
            scriptObject.Import(typeof(StringFunctions));

            Models.RenderModels.Object renderModel = RenderModel.FromNamedItem(builder, @object);
            scriptObject.Import(renderModel);

            TemplateContext templateContext = new TemplateContext();
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

                List<string> outputFilePaths = new List<string>(fileNameTemplates.Select(r => r.Render(renderModel)));

                RenderResultModel renderResult = new RenderResultModel { Result = result, BuilderConfig = builder, OutputPaths = outputFilePaths };

                return renderResult;
            } 
            else
            {
                Logger.LogError("Error finding template", "Could not find the following template: "+ builder.Input.Template);
                return null;
            }
        }
    }
}
