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
using DynamicCode.SourceGenerator.Engines;

namespace DynamicCode.SourceGenerator
{
    [Generator]
    public class DynamicCodeSourceGenerator : ISourceGenerator
    {
        private CodeGenerationConfig _config;
        private SourceFileSymbolVisitor _visitor;
        
        private RenderEngine _renderEngine;
        private GenerationEngine _generationEngine;

        public void Initialize(InitializationContext context)
        {
            Debugger.Launch();
            _renderEngine = new RenderEngine();
            _generationEngine = new GenerationEngine();
            _visitor = new SourceFileSymbolVisitor();
        }

        public void Execute(SourceGeneratorContext context)
        {
            _config = ConfigParser.GetConfig(_visitor, context);

            if (_config?.Builders == null)
                return;

            _generationEngine.NewGeneration();

            GenerateCode(context);
        }

        private void GenerateCode(SourceGeneratorContext context)
        {
            foreach (CodeGenerationConfigBuilder builder in _config.Builders)
            {
                if(builder.Input == null || builder.Output == null)
                {
                    Logger.LogError("Skipping generation", "Input or ouput object is not provided.", null);
                    continue;
                }


                foreach (INamedItem @object in _renderEngine.GetMatchedObjects(_visitor, context, builder))
                {
                    var renderResult = _renderEngine.RenderMatch(@object, builder);

                    if (renderResult != null)
                    {
                        _generationEngine.AddToCurrentGeneration(renderResult);
                    } 
                    else
                    { 
                        Logger.LogError("Skipping generation", "File failed to render.", null);
                        continue;
                    }
                }

                _generationEngine.PublishGeneration(context);
            }
        }
    }
}
