using System;
using System.Linq;
using DynamicCode.SourceGenerator.Models.Config;
using Microsoft.CodeAnalysis;
using DynamicCode.SourceGenerator.Metadata.Interfaces;
using DynamicCode.SourceGenerator.Metadata.Providers;

namespace DynamicCode.SourceGenerator.Metadata.Roslyn
{
    public class RoslynMetadataProvider : IMetadataProvider
    {
        private readonly Workspace workspace;

        public RoslynMetadataProvider()
        {

        }

        public IFileMetadata GetFile(string path, CodeGenerationConfig settings, Action<string[]> requestRender)
        {
            DocumentId document = workspace.CurrentSolution.GetDocumentIdsWithFilePath(path).FirstOrDefault();
            if (document != null)
            {
                return new RoslynFileMetadata(workspace.CurrentSolution.GetDocument(document), settings, requestRender);
            }

            return null;
        }
    }
}
