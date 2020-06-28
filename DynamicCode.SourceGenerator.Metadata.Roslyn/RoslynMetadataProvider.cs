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
        private readonly Workspace _workspace;

        public RoslynMetadataProvider(Workspace workspace)
        {
            _workspace = workspace;
        }

        public IFileMetadata GetFile(string path)
        {
            DocumentId document = _workspace.CurrentSolution.GetDocumentIdsWithFilePath(path).FirstOrDefault();
            if (document != null)
            {
                return new RoslynFileMetadata(_workspace.CurrentSolution.GetDocument(document));
            }

            return null;
        }
    }
}
