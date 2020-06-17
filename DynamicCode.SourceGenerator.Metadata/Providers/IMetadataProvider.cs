using DynamicCode.SourceGenerator.Models.Config;
using System;
using DynamicCode.SourceGenerator.Metadata.Interfaces;

namespace DynamicCode.SourceGenerator.Metadata.Providers
{
    public interface IMetadataProvider
    {
        IFileMetadata GetFile(string path, CodeGenerationConfig settings, Action<string[]> requestRender);
    }
}
