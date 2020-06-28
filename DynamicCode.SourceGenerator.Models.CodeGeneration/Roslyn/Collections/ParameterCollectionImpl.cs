using DynamicCode.SourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Collections
{
    public class ParameterCollectionImpl : ItemCollectionImpl<Parameter>, ParameterCollection
    {
        public ParameterCollectionImpl(IEnumerable<Parameter> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetAttributeFilter(Parameter item)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                yield return attribute.Name;
                yield return attribute.FullName;
            }
        }

        protected override IEnumerable<string> GetItemFilter(Parameter item)
        {
            yield return item.Name;
            yield return item.FullName;
        }
    }
}