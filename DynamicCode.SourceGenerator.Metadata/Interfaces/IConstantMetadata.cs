namespace DynamicCode.SourceGenerator.Metadata.Interfaces
{
    public interface IConstantMetadata : IFieldMetadata
    {
        string Value { get; }
    }
}