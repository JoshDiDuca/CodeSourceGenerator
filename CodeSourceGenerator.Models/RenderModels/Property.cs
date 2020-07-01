

using System.Collections.Generic;

namespace CodeSourceGenerator.Models.RenderModels
{
    /// <summary>
    /// Represents a property.
    /// </summary>
    public abstract class Property : Object
    {
        /// <summary>
        /// All attributes defined on the property.
        /// </summary>
        public abstract IEnumerable<Attribute> Attributes { get; }

        /// <summary>
        /// The XML documentation comment of the property.
        /// </summary>
        public abstract DocComment DocComment { get; }

        /// <summary>
        /// The full original name of the property including namespace and containing class names.
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// Determines if the property has a getter.
        /// </summary>
        public abstract bool HasGetter { get; }

        /// <summary>
        /// Determines if the property has a setter.
        /// </summary>
        public abstract bool HasSetter { get; }

        /// <summary>
        /// Determines if the property is abstract.
        /// </summary>
        public abstract bool IsAbstract { get; }

        /// <summary>
        /// Determines if the class is public.
        /// </summary>
        public abstract bool IsPublic { get; }

        /// <summary>
        /// Determines if the class is private.
        /// </summary>
        public abstract bool IsPrivate { get; }

        /// <summary>
        /// Determines if the class is protected.
        /// </summary>
        public abstract bool IsProtected { get; }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The parent context of the property.
        /// </summary>
        public abstract Object Parent { get; }

        /// <summary>
        /// The type of the property.
        /// </summary>
        public abstract Type Type { get; }

        /// <summary>
        /// Converts the current instance to string.
        /// </summary>
        public static implicit operator string (Property instance)
        {
            return instance.ToString();
        }
    }
}