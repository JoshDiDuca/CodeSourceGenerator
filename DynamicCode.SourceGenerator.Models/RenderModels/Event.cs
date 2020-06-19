using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models
{
    /// <summary>
    /// Represents an event.
    /// </summary>
    public abstract class Event : Object
    {
        /// <summary>
        /// All attributes defined on the event.
        /// </summary>
        public abstract IEnumerable<Attribute> Attributes { get; }

        /// <summary>
        /// The XML documentation comment of the event.
        /// </summary>
        public abstract DocComment DocComment { get; }

        /// <summary>
        /// The full original name of the event including namespace and containing class names.
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// The name of the event.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The parent context of the event.
        /// </summary>
        public abstract Object Parent { get; }

        /// <summary>
        /// Determines if the event acessability is public
        /// </summary>
        public abstract bool IsPublic { get; }

        /// <summary>
        /// Determines if the event acessability is private
        /// </summary>
        public abstract bool IsPrivate { get; }

        /// <summary>
        /// Determines if the event acessability is protected
        /// </summary>
        public abstract bool IsProtected { get; }

        /// <summary>
        /// The type of the event.
        /// </summary>
        public abstract Type Type { get; }

        /// <summary>
        /// Converts the current instance to string.
        /// </summary>
        public static implicit operator string (Event instance)
        {
            return instance.ToString();
        }
    }
}
