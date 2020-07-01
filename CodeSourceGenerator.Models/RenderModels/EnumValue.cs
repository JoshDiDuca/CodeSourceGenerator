

using System.Collections.Generic;

namespace CodeSourceGenerator.Models.RenderModels
{
    /// <summary>
    /// Represents an enum value.
    /// </summary>
    public abstract class EnumValue : Object
    {
        /// <summary>
        /// All attributes defined on the enum value.
        /// </summary>
        public abstract IEnumerable<Attribute> Attributes { get; }

        /// <summary>
        /// The XML documentation comment of the enum value.
        /// </summary>
        public abstract DocComment DocComment { get; }

        /// <summary>
        /// The full original name of the enum value including namespace and containing class names.
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// The name of the enum value.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The parent context of the enum value.
        /// </summary>
        public abstract Object Parent { get; }

        /// <summary>
        /// The numeric value.
        /// </summary>
        public abstract long Value { get; }

        /// <summary>
        /// Converts the current instance to string.
        /// </summary>
        public static implicit operator string (EnumValue instance)
        {
            return instance.ToString();
        }
    }

    /// <summary>
    /// Represents a collection of enum values.
    /// </summary>
    public interface EnumValueCollection : IEnumerable<EnumValue>
    {
    }
}