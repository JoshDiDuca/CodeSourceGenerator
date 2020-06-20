using DynamicCode.SourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;
using DynamicCode.SourceGenerator.Models.RenderModels;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class EnumImpl : Enum
    {
        private readonly IEnumMetadata _metadata;

        private EnumImpl(IEnumMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }

        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override string Namespace => _metadata.Namespace;

        private Type _type;
        protected override Type Type => _type ??= TypeImpl.FromMetadata(_metadata.Type, Parent);

        private bool? _isFlags;
        public override bool IsFlags => _isFlags ?? (_isFlags = Attributes.Any(a => a.FullName == "System.FlagsAttribute")).Value;

        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this);

        private DocComment _docComment;
        public override DocComment DocComment => _docComment ??= DocCommentImpl.FromXml(_metadata.DocComment, this);

        private EnumValueCollection _values;
        public override EnumValueCollection Values => _values ??= EnumValueImpl.FromMetadata(_metadata.Values, this);

        private Class _containingClass;
        public override Class ContainingClass => _containingClass ??= ClassImpl.FromMetadata(_metadata.ContainingClass, this);

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<Enum> FromMetadata(IEnumerable<IEnumMetadata> metadata, Object parent)
        {
            return new EnumCollectionImpl(metadata.Select(e => new EnumImpl(e, parent)));
        }

        public static Enum FromMetadata(IEnumMetadata metadata, Object parent)
        {
            return metadata == null ? null : new EnumImpl(metadata, parent);
        }
    }
}