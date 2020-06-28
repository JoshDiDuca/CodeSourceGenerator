using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Metadata.Interfaces
{
    public interface IFieldMetadata : INamedItem
    {
        string DocComment { get; }
        IEnumerable<IAttributeMetadata> Attributes { get; }
        ITypeMetadata Type { get; }
        bool IsPublic { get; }
        bool IsPrivate { get; }
        bool IsProtected { get; }
    }
}