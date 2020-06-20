using DynamicCode.SourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;
using DynamicCode.SourceGenerator.Models.RenderModels;
using LazyString = System.Lazy<string>;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class TypeImpl : Type
    {
        private readonly ITypeMetadata _metadata;
        private readonly LazyString _lazyName;
        private readonly LazyString _lazyTypescriptName;

        private TypeImpl(ITypeMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
            _lazyName = new LazyString(() => Helpers.GetOriginalName(metadata));
            _lazyTypescriptName = new LazyString(() => Helpers.GetTypeScriptName(metadata));
        }

        public override Object Parent { get; }

        public override string Name => _lazyName.Value.TrimStart('@');
        public override string TypescriptName => _lazyTypescriptName.Value;
        public override string FullName => _metadata.FullName;
        public override string Namespace => _metadata.Namespace;
        public override bool IsGeneric => _metadata.IsGeneric;
        public override bool IsEnum => _metadata.IsEnum;
        public override bool IsEnumerable => _metadata.IsEnumerable;
        public override bool IsNullable => _metadata.IsNullable;
        public override bool IsTask => _metadata.IsTask;
        public override bool IsPrimitive => Helpers.IsPrimitive(_metadata);
        public override bool IsDate => Name == "Date";
        public override bool IsDefined => _metadata.IsDefined;
        public override bool IsGuid => FullName == "System.Guid" || FullName == "System.Guid?";
        public override bool IsTimeSpan => FullName == "System.TimeSpan" || FullName == "System.TimeSpan?";
        public override bool IsValueTuple => _metadata.IsValueTuple;


        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this);

        private DocComment _docComment;
        public override DocComment DocComment => _docComment ??= DocCommentImpl.FromXml(_metadata.DocComment, this);

        private IEnumerable<Constant> _constants;
        public override IEnumerable<Constant> Constants => _constants ??= ConstantImpl.FromMetadata(_metadata.Constants, this);

        private IEnumerable<Delegate> _delegates;
        public override IEnumerable<Delegate> Delegates => _delegates ??= DelegateImpl.FromMetadata(_metadata.Delegates, this);

        private IEnumerable<Field> _fields;
        public override IEnumerable<Field> Fields => _fields ??= FieldImpl.FromMetadata(_metadata.Fields, this);

        private Class _baseClass;
        public override Class BaseClass => _baseClass ??= ClassImpl.FromMetadata(_metadata.BaseClass, this);

        private Class _containingClass;
        public override Class ContainingClass => _containingClass ??= ClassImpl.FromMetadata(_metadata.ContainingClass, this);

        private IEnumerable<Interface> _interfaces;
        public override IEnumerable<Interface> Interfaces => _interfaces ??= InterfaceImpl.FromMetadata(_metadata.Interfaces, this);

        private IEnumerable<Method> _methods;
        public override IEnumerable<Method> Methods => _methods ??= MethodImpl.FromMetadata(_metadata.Methods, this);

        private IEnumerable<Property> _properties;
        public override IEnumerable<Property> Properties => _properties ??= PropertyImpl.FromMetadata(_metadata.Properties, this);

        private IEnumerable<Type> _typeArguments;
        public override IEnumerable<Type> TypeArguments => _typeArguments ??= TypeImpl.FromMetadata(_metadata.TypeArguments, this);

        private IEnumerable<TypeParameter> _typeParameters;
        public override IEnumerable<TypeParameter> TypeParameters => _typeParameters ??= TypeParameterImpl.FromMetadata(_metadata.TypeParameters, this);

        private IEnumerable<Field> _tupleElements;
        public override IEnumerable<Field> TupleElements => _tupleElements ??= FieldImpl.FromMetadata(_metadata.TupleElements, this);

        private IEnumerable<Class> _nestedClasses;
        public override IEnumerable<Class> NestedClasses => _nestedClasses ??= ClassImpl.FromMetadata(_metadata.NestedClasses, this);

        private IEnumerable<Enum> _nestedEnums;
        public override IEnumerable<Enum> NestedEnums => _nestedEnums ??= EnumImpl.FromMetadata(_metadata.NestedEnums, this);

        private IEnumerable<Interface> _nestedInterfaces;
        public override IEnumerable<Interface> NestedInterfaces => _nestedInterfaces ??= InterfaceImpl.FromMetadata(_metadata.NestedInterfaces, this);
        
        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<Type> FromMetadata(IEnumerable<ITypeMetadata> metadata, Object parent)
        {
            return new TypeCollectionImpl(metadata.Select(t => new TypeImpl(t, parent)));
        }

        public static Type FromMetadata(ITypeMetadata metadata, Object parent)
        {
            return metadata == null ? null : new TypeImpl(metadata, parent);
        }
    }
}