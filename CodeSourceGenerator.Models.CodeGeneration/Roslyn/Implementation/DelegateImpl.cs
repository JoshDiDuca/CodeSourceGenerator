using CodeSourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CodeSourceGenerator.Models.CodeGeneration.Collections;
using CodeSourceGenerator.Models.RenderModels;

namespace CodeSourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class DelegateImpl : Delegate
    {
        private readonly IDelegateMetadata _metadata;

        private DelegateImpl(IDelegateMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }

        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override bool IsGeneric => _metadata.IsGeneric;

        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this);

        private DocComment _docComment;
        public override DocComment DocComment => _docComment ??= DocCommentImpl.FromXml(_metadata.DocComment, this);

        private IEnumerable<TypeParameter> _typeParameters;
        public override IEnumerable<TypeParameter> TypeParameters => _typeParameters ??= TypeParameterImpl.FromMetadata(_metadata.TypeParameters, this);

        private ParameterCollection _parameters;
        public override ParameterCollection Parameters => _parameters ??= ParameterImpl.FromMetadata(_metadata.Parameters, this);

        private Type _type;
        public override Type Type => _type ??= TypeImpl.FromMetadata(_metadata.Type, this);

        public override string ToString()
        {
            return Name;
        }

        public static DelegateCollection FromMetadata(IEnumerable<IDelegateMetadata> metadata, Object parent)
        {
            return new DelegateCollectionImpl(metadata.Select(d => new DelegateImpl(d, parent)));
        }
    }
}