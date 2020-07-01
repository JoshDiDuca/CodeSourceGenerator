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
using DynamicCode.SourceGenerator.Common.Logging;
using DynamicCode.SourceGenerator.Common.Logging.Factories;

namespace DynamicCode.SourceGenerator
{
    [Generator]
    public class DynamicCodeSourceGenerator : ISourceGenerator
    {
        private CodeGenerationConfig _config;
        private SourceFileSymbolVisitor _visitor;
        private Logger _logger;
        
        private RenderEngine _renderEngine;
        private GenerationEngine _generationEngine;

        public void Initialize(InitializationContext context)
        {
            Debugger.Launch();

            _logger = new Logger();
            _logger.RegisterFactory(new ConsoleLogOutputFactory("General Logging", LogScope.Error | LogScope.Warning | LogScope.Information));

            _renderEngine = new RenderEngine(_logger);
            _generationEngine = new GenerationEngine(_logger);

            _visitor = new SourceFileSymbolVisitor();
        }

        public void Execute(SourceGeneratorContext context)
        {
            _config = ConfigParser.GetConfig(_visitor, context);

            if (!string.IsNullOrEmpty(_config.Debugging?.LogOutput))
            {
                _logger.RegisterFactory(
                    new FileLogOutputFactory("Log Output", 
                    _config.Debugging.LogOutput, 
                    LogScope.Error | LogScope.Warning | LogScope.Information, 
                    LogScope.Objects, 
                    DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"))
                );
            }
            if (string.IsNullOrEmpty(_config.Debugging?.ObjectOutput))
            {
                _logger.RegisterFactory(
                    new FileLogOutputFactory("Object Output",
                    _config.Debugging?.ObjectOutput,
                    LogScope.Error | LogScope.Warning | LogScope.Information,
                    LogScope.Objects,
                    DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"))
                );
            }

            if (_config?.Builders == null)
            {
                _logger.LogError("Skipping generation", "No template config found.", null);
            }

            _generationEngine.NewGeneration();

            GenerateCode(context);
        }

        private void GenerateCode(SourceGeneratorContext context)
        {
            foreach (CodeGenerationConfigBuilder builder in _config.Builders)
            {
                if(builder.Input == null || builder.Output == null)
                {
                    _logger.LogError("Skipping generation", "Input or ouput object is not provided.", null);
                    continue;
                }

                foreach (INamedItem @object in _renderEngine.GetMatchedObjects(context, _visitor, builder))
                {
                    RenderResultModel renderResult = _renderEngine.RenderMatch(@object, builder);

                    if (renderResult != null)
                    {
                        _generationEngine.AddToCurrentGeneration(renderResult);
                    } 
                    else
                    {
                        _logger.LogError("Skipping generation", "File failed to render.", null);
                        continue;
                    }
                }

                _generationEngine.PublishGeneration(context);
            }
        }
    }
}
