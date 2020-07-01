using CodeSourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CodeSourceGenerator.Models.CodeGeneration.Collections;
using CodeSourceGenerator.Models.RenderModels;

namespace CodeSourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class PropertyImpl : Property
    {
        private readonly IPropertyMetadata _metadata;

        private PropertyImpl(IPropertyMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }
        
        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override bool HasGetter => _metadata.HasGetter;
        public override bool HasSetter => _metadata.HasSetter;
        public override bool IsAbstract => _metadata.IsAbstract;
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

        public static IEnumerable<Property> FromMetadata(IEnumerable<IPropertyMetadata> metadata, Object parent)
        {
            return new PropertyCollectionImpl(metadata.Select(p => new PropertyImpl(p, parent)));
        }
    }
}