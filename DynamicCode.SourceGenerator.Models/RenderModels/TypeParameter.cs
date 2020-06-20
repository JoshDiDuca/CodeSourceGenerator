namespace DynamicCode.SourceGenerator.Models.RenderModels
{
    /// <summary>
    /// Represents a generic type parameter.
    /// </summary>
    public abstract class TypeParameter : Object
    {
        /// <summary>
        /// The name of the type parameter.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The parent context of the type parameter.
        /// </summary>
        public abstract Object Parent { get; }
    }
}
