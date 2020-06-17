namespace DynamicCode.SourceGenerator.Metadata.Interfaces
{
    public interface IPropertyMetadata : IFieldMetadata
    {
        bool IsAbstract { get; }
        bool HasGetter { get; }
        bool HasSetter { get; }
    }
}