using DynamicCode.SourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;
using DynamicCode.SourceGenerator.Models.RenderModels;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class ClassImpl : Class
    {
        private readonly IClassMetadata _metadata;

        private ClassImpl(IClassMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }

        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override string Namespace => _metadata.Namespace;
        public override bool IsAbstract => _metadata.IsAbstract;
        public override bool IsGeneric => _metadata.IsGeneric;
        public override bool IsPublic => _metadata.IsPublic;
        public override bool IsPrivate => _metadata.IsPrivate;
        public override bool IsProtected => _metadata.IsProtected;

        private Type _type;
        protected override Type Type => _type ??= TypeImpl.FromMetadata(_metadata.Type, Parent);
        
        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this).ToList();

        private IEnumerable<Constant> _constants;
        public override IEnumerable<Constant> Constants => _constants ??= ConstantImpl.FromMetadata(_metadata.Constants, this).ToList();

        private IEnumerable<Delegate> _delegates;
        public override IEnumerable<Delegate> Delegates => _delegates ??= DelegateImpl.FromMetadata(_metadata.Delegates, this).ToList();

        private DocComment _docComment;
        public override DocComment DocComment => _docComment ??= DocCommentImpl.FromXml(_metadata.DocComment, this);

        private IEnumerable<Event> _events;
        public override IEnumerable<Event> Events => _events ??= EventImpl.FromMetadata(_metadata.Events, this).ToList();

        private IEnumerable<Field> _fields;
        public override IEnumerable<Field> Fields => _fields ??= FieldImpl.FromMetadata(_metadata.Fields, this).ToList();

        private Class _baseClass;
        public override Class BaseClass => _baseClass ??= ClassImpl.FromMetadata(_metadata.BaseClass, this);

        private Class _containingClass;
        public override Class ContainingClass => _containingClass ??= ClassImpl.FromMetadata(_metadata.ContainingClass, this);

        private IEnumerable<Interface> _interfaces;
        public override IEnumerable<Interface> Interfaces => _interfaces ??= InterfaceImpl.FromMetadata(_metadata.Interfaces, this).ToList();

        private IEnumerable<Method> _methods;
        public override IEnumerable<Method> Methods => _methods ??= MethodImpl.FromMetadata(_metadata.Methods, this).ToList();

        private IEnumerable<Property> _properties;
        public override IEnumerable<Property> Properties => _properties ??= PropertyImpl.FromMetadata(_metadata.Properties, this).ToList();

        private IEnumerable<TypeParameter> _typeParameters;
        public override IEnumerable<TypeParameter> TypeParameters => _typeParameters ??= TypeParameterImpl.FromMetadata(_metadata.TypeParameters, this).ToList();

        private IEnumerable<Type> _typeArguments;
        public override IEnumerable<Type> TypeArguments => _typeArguments ??= TypeImpl.FromMetadata(_metadata.TypeArguments, this).ToList();

        private IEnumerable<Class> _nestedClasses;
        public override IEnumerable<Class> NestedClasses => _nestedClasses ??= ClassImpl.FromMetadata(_metadata.NestedClasses, this).ToList();

        private IEnumerable<Enum> _nestedEnums;
        public override IEnumerable<Enum> NestedEnums => _nestedEnums ??= EnumImpl.FromMetadata(_metadata.NestedEnums, this).ToList();

        private IEnumerable<Interface> _nestedInterfaces;
        public override IEnumerable<Interface> NestedInterfaces => _nestedInterfaces ??= InterfaceImpl.FromMetadata(_metadata.NestedInterfaces, this).ToList();

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<Class> FromMetadata(IEnumerable<IClassMetadata> metadata, Object parent)
        {
            return new ClassCollectionImpl(metadata.Select(c => new ClassImpl(c, parent)));
        }

        public static Class FromMetadata(IClassMetadata metadata, Object parent)
        {
            return metadata == null ? null : new ClassImpl(metadata, parent);
        }
    }
}
