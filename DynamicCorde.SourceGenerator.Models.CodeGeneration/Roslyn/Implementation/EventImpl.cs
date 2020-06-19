using DynamicCode.SourceGenerator.Metadata.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Collections;

namespace DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation
{
    public sealed class EventImpl : Event
    {
        private readonly IEventMetadata _metadata;

        private EventImpl(IEventMetadata metadata, Object parent)
        {
            _metadata = metadata;
            Parent = parent;
        }

        public override Object Parent { get; }

        public override string Name => _metadata.Name.TrimStart('@');
        public override string FullName => _metadata.FullName;

        private IEnumerable<Attribute> _attributes;
        public override IEnumerable<Attribute> Attributes => _attributes ??= AttributeImpl.FromMetadata(_metadata.Attributes, this);

        private DocComment _docComment;
        public override DocComment DocComment => _docComment ??= DocCommentImpl.FromXml(_metadata.DocComment, this);

        private Type _type;
        public override Type Type => _type ??= TypeImpl.FromMetadata(_metadata.Type, this);

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<Event> FromMetadata(IEnumerable<IEventMetadata> metadata, Object parent)
        {
            return new EventCollectionImpl(metadata.Select(e => new EventImpl(e, parent)));
        }
    }
}