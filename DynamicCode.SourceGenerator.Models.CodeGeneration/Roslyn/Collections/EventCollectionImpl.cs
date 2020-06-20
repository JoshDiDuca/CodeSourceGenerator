using DynamicCode.SourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Collections
{
    public class EventCollectionImpl : ItemCollectionImpl<Event>, IEnumerable<Event>
    {
        public EventCollectionImpl(IEnumerable<Event> values) : base(values)
        {
        }

        protected override IEnumerable<string> GetAttributeFilter(Event item)
        {
            foreach (var attribute in item.Attributes)
            {
                yield return attribute.Name;
                yield return attribute.FullName;
            }
        }

        protected override IEnumerable<string> GetItemFilter(Event item)
        {
            yield return item.Name;
            yield return item.FullName;
        }
    }
}