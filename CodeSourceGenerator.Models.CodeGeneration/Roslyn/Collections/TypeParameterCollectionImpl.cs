using CodeSourceGenerator.Models.RenderModels;
using System.Collections.Generic;
using System.Linq;

namespace CodeSourceGenerator.Models.CodeGeneration.Collections
{
    public class TypeParameterCollectionImpl : ItemCollectionImpl<TypeParameter>, IEnumerable<TypeParameter>
    {
        public TypeParameterCollectionImpl(IEnumerable<TypeParameter> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetItemFilter(TypeParameter item)
        {
            yield return item.Name;
        }

        public override string ToString()
        {
            if(Count == 0)
                return string.Empty;

            return string.Concat("<", string.Join(", ", this.Select(t => t.Name)), ">");
        }
    }
}