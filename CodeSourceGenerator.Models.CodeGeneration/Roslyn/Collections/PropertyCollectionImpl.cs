using CodeSourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace CodeSourceGenerator.Models.CodeGeneration.Collections
{
    public class PropertyCollectionImpl : ItemCollectionImpl<Property>, IEnumerable<Property>
    {
        public PropertyCollectionImpl(IEnumerable<Property> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetAttributeFilter(Property item)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                yield return attribute.Name;
                yield return attribute.FullName;
            }
        }

        protected override IEnumerable<string> GetItemFilter(Property item)
        {
            yield return item.Name;
            yield return item.FullName;
        }
    }
}