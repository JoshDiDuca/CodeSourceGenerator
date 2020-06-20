using DynamicCode.SourceGenerator.Models.RenderModels;
using System.Collections.Generic;
using System.Linq;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Collections
{
    public class TypeCollectionImpl : ItemCollectionImpl<Type>, IEnumerable<Type>
    {
        public TypeCollectionImpl(IEnumerable<Type> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetAttributeFilter(Type item)
        {
            foreach (var attribute in item.Attributes)
            {
                yield return attribute.Name;
                yield return attribute.FullName;
            }
        }

        protected override IEnumerable<string> GetItemFilter(Type item)
        {
            yield return item.TypescriptName;
            yield return item.FullName;
        }

        public override string ToString()
        {
            if(Count == 0)
                return string.Empty;

            return string.Concat("<", string.Join(", ", this.Select(t => t.Name)), ">");
        }
    }
}