namespace CodeSourceGenerator.Metadata.Interfaces
{
    public interface IConstantMetadata : IFieldMetadata
    {
        string Value { get; }
    }
}