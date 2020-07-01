using CodeSourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace CodeSourceGenerator.Models.CodeGeneration.Collections
{
    public abstract class ItemCollectionImpl<T> : List<T> where T : Object
    {
        protected ItemCollectionImpl(IEnumerable<T> values) : base(values)
        {
        }

        public System.Func<Object, IEnumerable<string>> AttributeFilterSelector => i =>
        {
            return !(i is T ty) ? new string[0] : GetAttributeFilter(ty);
        };

        public System.Func<Object, IEnumerable<string>> InheritanceFilterSelector => i =>
        {
            return !(i is T ty) ? new string[0] : GetInheritanceFilter(ty);
        };

        public System.Func<Object, IEnumerable<string>> ItemFilterSelector => i =>
        {
            return !(i is T ty) ? new string[0] : GetItemFilter(ty);
        };

        protected virtual IEnumerable<string> GetAttributeFilter(T item)
        {
            return new string[0];
        }

        protected virtual IEnumerable<string> GetInheritanceFilter(T item)
        {
            return new string[0];
        }

        protected virtual IEnumerable<string> GetItemFilter(T item)
        {
            return new string[0];
        }
    }
}