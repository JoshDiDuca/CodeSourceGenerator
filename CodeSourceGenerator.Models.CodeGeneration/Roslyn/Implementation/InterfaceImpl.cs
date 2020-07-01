using CodeSourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CodeSourceGenerator.Models.CodeGeneration.Collections;
using CodeSourceGenerator.Models.RenderModels;

namespace CodeSourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class InterfaceImpl : Interface
    {
        private readonly IInterfaceMetadata _metadata;

        private InterfaceImpl(IInterfaceMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }

        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override string Namespace => _metadata.Namespace;
        public override bool IsGeneric => _metadata.IsGeneric;
        public override bool IsPublic => _metadata.IsPublic;
        public override bool IsPrivate => _metadata.IsPrivate;
        public override bool IsProtected => _metadata.IsProtected;

        private Type _type;
        protected override Type Type => _type ??= TypeImpl.FromMetadata(_metadata.Type, Parent);

        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this);

        private DocComment _docComment;
        public override DocComment DocComment => _docComment ??= DocCommentImpl.FromXml(_metadata.DocComment, this);

        private IEnumerable<Event> _events;
        public override IEnumerable<Event> Events => _events ??= EventImpl.FromMetadata(_metadata.Events, this);

        private IEnumerable<Interface> _interfaces;
        public override IEnumerable<Interface> Interfaces => _interfaces ??= InterfaceImpl.FromMetadata(_metadata.Interfaces, this);

        private IEnumerable<Method> _methods;
        public override IEnumerable<Method> Methods => _methods ??= MethodImpl.FromMetadata(_metadata.Methods, this);

        private IEnumerable<Property> _properties;
        public override IEnumerable<Property> Properties => _properties ??= PropertyImpl.FromMetadata(_metadata.Properties, this);

        private IEnumerable<TypeParameter> _typeParameters;
        public override IEnumerable<TypeParameter> TypeParameters => _typeParameters ??= TypeParameterImpl.FromMetadata(_metadata.TypeParameters, this);

        private IEnumerable<Type> _typeArguments;
        public override IEnumerable<Type> TypeArguments => _typeArguments ??= TypeImpl.FromMetadata(_metadata.TypeArguments, this);

        private Class _containingClass;
        public override Class ContainingClass => _containingClass ??= ClassImpl.FromMetadata(_metadata.ContainingClass, this);

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<Interface> FromMetadata(IEnumerable<IInterfaceMetadata> metadata, Object parent)
        {
            return new InterfaceCollectionImpl(metadata.Select(i => new InterfaceImpl(i, parent)));
        }

        public static Interface FromMetadata(IInterfaceMetadata metadata, Object parent)
        {
            return metadata == null ? null : new InterfaceImpl(metadata, parent);
        }
    }
}