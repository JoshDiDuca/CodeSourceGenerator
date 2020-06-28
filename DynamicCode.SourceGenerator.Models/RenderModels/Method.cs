
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.RenderModels
{
    /// <summary>
    /// Represents a method.
    /// </summary>
    public abstract class Method : Object
    {
        /// <summary>
        /// All attributes defined on the method.
        /// </summary>
        public abstract IEnumerable<Attribute> Attributes { get; }

        /// <summary>
        /// The XML documentation comment of the method.
        /// </summary>
        public abstract DocComment DocComment { get; }

        /// <summary>
        /// The full original name of the method including namespace and containing class names.
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// Determines if the method is abstract.
        /// </summary>
        public abstract bool IsAbstract { get; }

        /// <summary>
        /// Determines if the method is generic.
        /// </summary>
        public abstract bool IsGeneric { get; }

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
        /// The name of the method.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// All parameters of the method.
        /// </summary>
        public abstract ParameterCollection Parameters { get; }

        /// <summary>
        /// The parent context of the method.
        /// </summary>
        public abstract Object Parent { get; }

        /// <summary>
        /// The type of the method.
        /// </summary>
        public abstract Type Type { get; }

        /// <summary>
        /// All generic type parameters of the method.
        /// TypeParameters are the type placeholders of a generic method e.g. &lt;T&gt;.
        /// </summary>
        public abstract IEnumerable<TypeParameter> TypeParameters { get; }

        /// <summary>
        /// Converts the current instance to string.
        /// </summary>
        public static implicit operator string (Method instance)
        {
            return instance.ToString();
        }
    }
}