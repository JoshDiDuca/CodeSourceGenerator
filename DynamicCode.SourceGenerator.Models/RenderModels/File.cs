using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.RenderModels
{
    /// <summary>
    /// Represents a file.
    /// </summary>
    public abstract class File : Object
    {
        /// <summary>
        /// All public classes defined in the file.
        /// </summary>
        public abstract IEnumerable<Class> Classes { get; }

        /// <summary>
        /// All public delegates defined in the file.
        /// </summary>
        public abstract DelegateCollection Delegates { get; }

        /// <summary>
        /// All public enums defined in the file.
        /// </summary>
        public abstract IEnumerable<Enum> Enums { get; }

        /// <summary>
        /// All public interfaces defined in the file.
        /// </summary>
        public abstract IEnumerable<Interface> Interfaces { get; }
        
        /// <summary>
        /// The full path of the file.
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public abstract string Name { get; }
    }
}