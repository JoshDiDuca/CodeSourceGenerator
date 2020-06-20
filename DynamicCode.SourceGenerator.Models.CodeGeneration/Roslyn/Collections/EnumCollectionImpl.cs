using DynamicCode.SourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Collections
{
    public class EnumCollectionImpl : ItemCollectionImpl<Enum>, IEnumerable<Enum>
    {
        public EnumCollectionImpl(IEnumerable<Enum> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetAttributeFilter(Enum item)
        {
            foreach (var attribute in item.Attributes)
            {
                yield return attribute.Name;
                yield return attribute.FullName;
            }
        }

        protected override IEnumerable<string> GetItemFilter(Enum item)
        {
            yield return item.Name;
            yield return item.FullName;
        }
    }
}