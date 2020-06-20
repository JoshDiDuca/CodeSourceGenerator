using DynamicCode.SourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;
using DynamicCode.SourceGenerator.Models;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class ParameterImpl : Parameter
    {
        private readonly IParameterMetadata _metadata;

        private ParameterImpl(IParameterMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }

        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override bool HasDefaultValue => _metadata.HasDefaultValue;
        public override string DefaultValue => _metadata.DefaultValue;

        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this);

        private Type _type;
        public override Type Type => _type ??= TypeImpl.FromMetadata(_metadata.Type, this);

        public override string ToString()
        {
            return Name;
        }

        public static ParameterCollection FromMetadata(IEnumerable<IParameterMetadata> metadata, Object parent)
        {
            return new ParameterCollectionImpl(metadata.Select(p => new ParameterImpl(p, parent)));
        }
    }
}