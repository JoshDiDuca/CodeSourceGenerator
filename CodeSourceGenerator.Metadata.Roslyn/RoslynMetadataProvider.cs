using System;
using System.Linq;
using CodeSourceGenerator.Models.Config;
using Microsoft.CodeAnalysis;
using CodeSourceGenerator.Metadata.Interfaces;
using CodeSourceGenerator.Metadata.Providers;

namespace CodeSourceGenerator.Metadata.Roslyn
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
