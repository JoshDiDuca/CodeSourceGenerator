using CodeSourceGenerator.Models.Config;
using System;
using CodeSourceGenerator.Metadata.Interfaces;

namespace CodeSourceGenerator.Metadata.Providers
{
    public interface IMetadataProvider
    {
        IFileMetadata GetFile(string path);
    }
}
