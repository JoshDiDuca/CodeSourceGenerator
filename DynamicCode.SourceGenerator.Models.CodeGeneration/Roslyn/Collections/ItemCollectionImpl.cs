using DynamicCode.SourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Collections
{
    public abstract class ItemCollectionImpl<T> : List<T> where T : Object
    {
        protected ItemCollectionImpl(IEnumerable<T> values) : base(values)
        {
        }

        public System.Func<Object, IEnumerable<string>> AttributeFilterSelector => i =>
        {
            var item = i as T;
            return item == null ? new string[0] : GetAttributeFilter(item);
        };

        public System.Func<Object, IEnumerable<string>> InheritanceFilterSelector => i =>
        {
            var item = i as T;
            return item == null ? new string[0] : GetInheritanceFilter(item);
        };

        public System.Func<Object, IEnumerable<string>> ItemFilterSelector => i =>
        {
            var item = i as T;
            return item == null ? new string[0] : GetItemFilter(item);
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