using DynamicCode.SourceGenerator.Metadata.Interfaces;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class EnumValueImpl : EnumValue
    {
        private readonly IEnumValueMetadata _metadata;

        private EnumValueImpl(IEnumValueMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }


        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override long Value => _metadata.Value;

        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this);

        private DocComment _docComment;
        public override DocComment DocComment => _docComment ??= DocCommentImpl.FromXml(_metadata.DocComment, this);

        public override string ToString()
        {
            return Name;
        }

        public static EnumValueCollection FromMetadata(IEnumerable<IEnumValueMetadata> metadata, Object parent)
        {
            return new EnumValueCollectionImpl(metadata.Select(e => new EnumValueImpl(e, parent)));
        }
    }
}