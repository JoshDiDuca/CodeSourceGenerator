using CodeSourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace CodeSourceGenerator.Models.CodeGeneration.Collections
{
    public class InterfaceCollectionImpl : ItemCollectionImpl<Interface>, IEnumerable<Interface>
    {
        public InterfaceCollectionImpl(IEnumerable<Interface> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetAttributeFilter(Interface item)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                yield return attribute.Name;
                yield return attribute.FullName;
            }
        }

        protected override IEnumerable<string> GetInheritanceFilter(Interface item)
        {
            foreach (Interface implementedInterface in item.Interfaces)
            {
                yield return implementedInterface.Name;
                yield return implementedInterface.FullName;
            }
        }

        protected override IEnumerable<string> GetItemFilter(Interface item)
        {
            yield return item.Name;
            yield return item.FullName;
        }
    }
}