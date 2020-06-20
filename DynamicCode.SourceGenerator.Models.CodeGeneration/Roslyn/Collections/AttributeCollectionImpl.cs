using DynamicCode.SourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Collections
{
    public class AttributeCollectionImpl : ItemCollectionImpl<Attribute>, IEnumerable<Attribute>
    {
        public AttributeCollectionImpl(IEnumerable<Attribute> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetItemFilter(Attribute item)
        {
            yield return item.Name;
            yield return item.FullName;
        }
    }

    public class ParameterCommentCollectionImpl : ItemCollectionImpl<ParameterComment>, ParameterCommentCollection
    {
        public ParameterCommentCollectionImpl(IEnumerable<ParameterComment> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetItemFilter(ParameterComment item)
        {
            yield return item.Name;
        }
    }
}