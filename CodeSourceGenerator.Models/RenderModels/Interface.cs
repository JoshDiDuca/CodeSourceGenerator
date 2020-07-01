using System.Collections.Generic;

namespace CodeSourceGenerator.Models.RenderModels
{
    /// <summary>
    /// Represents an interface.
    /// </summary>
    public abstract class Interface : Object
    {
        /// <summary>
        /// All attributes defined on the interface.
        /// </summary>
        public abstract IEnumerable<Attribute> Attributes { get; }

        /// <summary>
        /// The containing class of the interface if it is nested.
        /// </summary>
        public abstract Class ContainingClass { get; }

        /// <summary>
        /// The XML documentation comment of the interface.
        /// </summary>
        public abstract DocComment DocComment { get; }

        /// <summary>
        /// All events defined in the interface.
        /// </summary>
        public abstract IEnumerable<Event> Events { get; }

        /// <summary>
        /// The full original name of the interface including namespace and containing class names.
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// All interfaces implemented by the interface.
        /// </summary>
        public abstract IEnumerable<Interface> Interfaces { get; }

        /// <summary>
        /// Determines if the interface is generic.
        /// </summary>
        public abstract bool IsGeneric { get; }

        /// <summary>
        /// Determines if the interface is public.
        /// </summary>
        public abstract bool IsPublic { get; }

        /// <summary>
        /// Determines if the interface is private.
        /// </summary>
        public abstract bool IsPrivate { get; }

        /// <summary>
        /// Determines if the interface is protected.
        /// </summary>
        public abstract bool IsProtected { get; }

        /// <summary>
        /// All methods defined in the interface.
        /// </summary>
        public abstract IEnumerable<Method> Methods { get; }

        /// <summary>
        /// The name of the interface.
        /// </summary>
        public abstract string Name { get; }
        
        /// <summary>
        /// The namespace of the interface.
        /// </summary>
        public abstract string Namespace { get; }

        /// <summary>
        /// The parent context of the interface.
        /// </summary>
        public abstract Object Parent { get; }

        /// <summary>
        /// All properties defined in the interface.
        /// </summary>
        public abstract IEnumerable<Property> Properties { get; }

        /// <summary>
        /// All generic type arguments of the interface.
        /// TypeArguments are the specified arguments for the TypeParametes on a generic interface e.g. &lt;string&gt;.
        /// (In Visual Studio 2013 TypeParameters and TypeArguments are the same)
        /// </summary>
        public abstract IEnumerable<Type> TypeArguments { get; }

        /// <summary>
        /// All generic type parameters of the interface.
        /// TypeParameters are the type placeholders of a generic interface e.g. &lt;T&gt;.
        /// (In Visual Studio 2013 TypeParameters and TypeArguments are the same)
        /// </summary>
        public abstract IEnumerable<TypeParameter> TypeParameters { get; }

        /// <summary>
        /// Converts the current instance to string.
        /// </summary>
        public static implicit operator string (Interface instance)
        {
            return instance.ToString();
        }

        protected abstract Type Type { get; }

        /// <summary>
        /// Converts the current instance to a Type.
        /// </summary>
        public static implicit operator Type(Interface instance)
        {
            return instance?.Type;
        }
    }
}