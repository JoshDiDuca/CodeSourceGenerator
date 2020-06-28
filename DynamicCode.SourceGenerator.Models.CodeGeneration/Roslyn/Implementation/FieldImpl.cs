using DynamicCode.SourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;
using DynamicCode.SourceGenerator.Models.RenderModels;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class FieldImpl : Field
    {
        private readonly IFieldMetadata _metadata;

        private FieldImpl(IFieldMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }


        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override bool IsPublic => _metadata.IsPublic;
        public override bool IsPrivate => _metadata.IsPrivate;
        public override bool IsProtected => _metadata.IsProtected;

        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this);

        private DocComment _docComment;
        public override DocComment DocComment => _docComment ??= DocCommentImpl.FromXml(_metadata.DocComment, this);

        private Type _type;
        public override Type Type => _type ??= TypeImpl.FromMetadata(_metadata.Type, this);

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<Field> FromMetadata(IEnumerable<IFieldMetadata> metadata, Object parent)
        {
            return new FieldCollectionImpl(metadata.Select(f => new FieldImpl(f, parent)));
        }
    }
}