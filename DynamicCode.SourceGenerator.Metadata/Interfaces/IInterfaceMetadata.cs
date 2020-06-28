using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Metadata.Interfaces
{
    public interface IInterfaceMetadata : INamedItem
    {
        string DocComment { get; }
        string Namespace { get; }
        bool IsGeneric { get; }
        bool IsPublic { get; }
        bool IsPrivate { get; }
        bool IsProtected { get; }
        ITypeMetadata Type { get; }
        IEnumerable<IAttributeMetadata> Attributes { get; }
        IClassMetadata ContainingClass { get; }
        IEnumerable<IEventMetadata> Events { get; }
        IEnumerable<ITypeParameterMetadata> TypeParameters { get; }
        IEnumerable<ITypeMetadata> TypeArguments { get; }
        IEnumerable<IInterfaceMetadata> Interfaces { get; }
        IEnumerable<IMethodMetadata> Methods { get; }
        IEnumerable<IPropertyMetadata> Properties { get; }
    }
}