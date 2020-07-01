
using System.Collections.Generic;

namespace CodeSourceGenerator.Models.RenderModels
{
    /// <summary>
    /// Represents a field.
    /// </summary>
    public abstract class Field : Object
    {
        /// <summary>
        /// All attributes defined on the field.
        /// </summary>
        public abstract IEnumerable<Attribute> Attributes { get; }

        /// <summary>
        /// The XML documentation comment of the field.
        /// </summary>
        public abstract DocComment DocComment { get; }

        /// <summary>
        /// The full original name of the field including namespace and containing class names.
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// The name of the field.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The parent context of the field.
        /// </summary>
        public abstract Object Parent { get; }

        /// <summary>
        /// The type of the field.
        /// </summary>
        public abstract Type Type { get; }

        /// <summary>
        /// Determines if the field is public.
        /// </summary>
        public abstract bool IsPublic { get; }

        /// <summary>
        /// Determines if the field is private.
        /// </summary>
        public abstract bool IsPrivate { get; }

        /// <summary>
        /// Determines if the field is protected.
        /// </summary>
        public abstract bool IsProtected { get; }

        /// <summary>
        /// Converts the current instance to string.
        /// </summary>
        public static implicit operator string (Field instance)
        {
            return instance.ToString();
        }
    }

    /// <summary>
    /// Represents a collection of fields.
    /// </summary>
    public interface FieldCollection : IEnumerable<Field>
    {
    }
}