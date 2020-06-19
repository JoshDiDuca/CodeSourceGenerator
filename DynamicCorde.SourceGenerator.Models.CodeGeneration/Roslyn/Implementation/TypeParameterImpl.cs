using DynamicCode.SourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public class TypeParameterImpl : TypeParameter
    {
        private readonly ITypeParameterMetadata _metadata;

        public TypeParameterImpl(ITypeParameterMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }
        public override string Name => _metadata.Name.TrimStart('@');

        public static IEnumerable<TypeParameter> FromMetadata(IEnumerable<ITypeParameterMetadata> metadata, Object parent)
        {
            return new TypeParameterCollectionImpl(metadata.Select(t => new TypeParameterImpl(t, parent)));
        }
    }
}