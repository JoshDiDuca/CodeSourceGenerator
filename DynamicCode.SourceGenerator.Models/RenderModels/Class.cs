
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models
{
    public abstract class Class : Object
    {
        /// <summary>
        /// All attributes defined on the class.
        /// </summary>
        public abstract IEnumerable<Attribute> Attributes { get; }

        /// <summary>
        /// The base class of the class.
        /// </summary>
        public abstract Class BaseClass { get; }

        /// <summary>
        /// All constants defined in the class.
        /// </summary>
        public abstract IEnumerable<Constant> Constants { get; }

        /// <summary>
        /// The containing class of the class if it's nested.
        /// </summary>
        public abstract Class ContainingClass { get; }

        /// <summary>
        /// All delegates defined in the class.
        /// </summary>
        public abstract IEnumerable<Delegate> Delegates { get; }

        /// <summary>
        /// The XML documentation comment of the class.
        /// </summary>
        public abstract DocComment DocComment { get; }

        /// <summary>
        /// All events defined in the class.
        /// </summary>
        public abstract IEnumerable<Event> Events { get; }

        /// <summary>
        /// All fields defined in the class.
        /// </summary>
        public abstract IEnumerable<Field> Fields { get; }

        /// <summary>
        /// The full original name of the class including namespace and containing class names.
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// All interfaces implemented by the class.
        /// </summary>
        public abstract IEnumerable<Interface> Interfaces { get; }

        /// <summary>
        /// Determines if the class is abstract.
        /// </summary>
        public abstract bool IsAbstract { get; }

        /// <summary>
        /// Determines if the class is generic.
        /// </summary>
        public abstract bool IsGeneric { get; }

        /// <summary>
        /// Determines if the class acessability is public
        /// </summary>
        public abstract bool IsPublic { get; }

        /// <summary>
        /// Determines if the class acessability is private
        /// </summary>
        public abstract bool IsPrivate { get; }

        /// <summary>
        /// Determines if the class acessability is protected
        /// </summary>
        public abstract bool IsProtected { get; }

        /// <summary>
        /// All methods defined in the class.
        /// </summary>
        public abstract IEnumerable<Method> Methods { get; }

        /// <summary>
        /// The name of the class.
        /// </summary>
        public abstract string Name { get; }
        
        /// <summary>
        /// The namespace of the class.
        /// </summary>
        public abstract string Namespace { get; }

        /// <summary>
        /// All classes defined in the class.
        /// </summary>
        public abstract IEnumerable<Class> NestedClasses { get; }

        /// <summary>
        /// All enums defined in the class.
        /// </summary>
        public abstract IEnumerable<Enum> NestedEnums { get; }

        /// <summary>
        /// All interfaces defined in the class.
        /// </summary>
        public abstract IEnumerable<Interface> NestedInterfaces { get; }

        /// <summary>
        /// The parent context of the class.
        /// </summary>
        public abstract Object Parent { get; }

        /// <summary>
        /// All properties defined in the class.
        /// </summary>
        public abstract IEnumerable<Property> Properties { get; }

        /// <summary>
        /// All generic type arguments of the class.
        /// TypeArguments are the specified arguments for the TypeParametes on a generic class e.g. &lt;string&gt;.
        /// (In Visual Studio 2013 TypeParameters and TypeArguments are the same)
        /// </summary>
        public abstract IEnumerable<Type> TypeArguments { get; }

        /// <summary>
        /// All generic type parameters of the class.
        /// TypeParameters are the type placeholders of a generic class e.g. &lt;T&gt;.
        /// (In Visual Studio 2013 TypeParameters and TypeArguments are the same)
        /// </summary>
        public abstract IEnumerable<TypeParameter> TypeParameters { get; }


        /// <summary>
        /// Converts the current instance to string.
        /// </summary>
        public static implicit operator string(Class instance)
        {
            return instance.ToString();
        }

        protected abstract Type Type { get; }

        /// <summary>
        /// Converts the current instance to a Type.
        /// </summary>
        public static implicit operator Type(Class instance)
        {
            return instance?.Type;
        }
    }
}