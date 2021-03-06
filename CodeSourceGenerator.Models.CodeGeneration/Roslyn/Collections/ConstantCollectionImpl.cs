using CodeSourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace CodeSourceGenerator.Models.CodeGeneration.Collections
{
    public class ConstantCollectionImpl : ItemCollectionImpl<Constant>, IEnumerable<Constant>
    {
        public ConstantCollectionImpl(IEnumerable<Constant> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetAttributeFilter(Constant item)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                yield return attribute.Name;
                yield return attribute.FullName;
            }
        }

        protected override IEnumerable<string> GetItemFilter(Constant item)
        {
            yield return item.Name;
            yield return item.FullName;
        }
    }
}