using System.Collections.Generic;

namespace CodeSourceGenerator.Metadata.Interfaces
{
    public interface IEventMetadata : INamedItem
    {
        string DocComment { get; }
        IEnumerable<IAttributeMetadata> Attributes { get; }
        ITypeMetadata Type { get; }
    }
}