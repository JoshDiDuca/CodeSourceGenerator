using DynamicCode.SourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;
using DynamicCode.SourceGenerator.Models.RenderModels;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class AttributeImpl : Attribute
    {
        private readonly IAttributeMetadata _metadata;

        private AttributeImpl(IAttributeMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }

        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;
        public override string Value => GetValue(_metadata.Value);

        private static string GetValue(string value)
        {
            if (value == null) return null;

            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                var trimmed = value.Substring(1, value.Length - 2);

                if (trimmed.Replace("\\\"", string.Empty).Contains("\"") == false)
                    return trimmed;
            }

            return value;
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<Attribute> FromMetadata(IEnumerable<IAttributeMetadata> metadata, Object parent)
        {
            return new AttributeCollectionImpl(metadata.Select(a => new AttributeImpl(a, parent)));
        }
    }
}