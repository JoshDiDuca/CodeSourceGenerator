using DynamicCode.SourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Collections
{
    public class ClassCollectionImpl : ItemCollectionImpl<Class>, IEnumerable<Class>
    {
        public ClassCollectionImpl(IEnumerable<Class> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetAttributeFilter(Class item)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                yield return attribute.Name;
                yield return attribute.FullName;
            }
        }

        protected override IEnumerable<string> GetInheritanceFilter(Class item)
        {
            if (item.BaseClass != null)
            {
                yield return item.BaseClass.Name;
                yield return item.BaseClass.FullName;
            }

            foreach (Interface implementedInterface in item.Interfaces)
            {
                yield return implementedInterface.Name;
                yield return implementedInterface.FullName;
            }
        }

        protected override IEnumerable<string> GetItemFilter(Class item)
        {
            yield return item.Name;
            yield return item.FullName;
        }
    }
}