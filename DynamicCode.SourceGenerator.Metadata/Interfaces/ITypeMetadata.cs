using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Metadata.Interfaces
{
    public interface ITypeMetadata : IClassMetadata
    {
        bool IsEnum { get; }
        bool IsEnumerable { get; }
        bool IsNullable { get; }
        bool IsPublic { get; }
        bool IsPrivate { get; }
        bool IsProtected { get; }
        bool IsTask { get; }
        bool IsDefined { get; }
        bool IsValueTuple { get; }
        IEnumerable<IFieldMetadata> TupleElements { get; }
    }
}