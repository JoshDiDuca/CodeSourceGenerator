using CodeSourceGenerator.Metadata.Interfaces;
using CodeSourceGenerator.Models;
using CodeSourceGenerator.Models.CodeGeneration.Implementation;
using CodeSourceGenerator.Models.RenderModels;
using System.Collections.Generic;

namespace CodeSourceGenerator.CodeGeneration.Implementation
{
    public sealed class FileImpl : File
    {
        private readonly IFileMetadata _metadata;

        public FileImpl(IFileMetadata metadata)
        {
            _metadata = metadata;
        }

        public override string Name => _metadata.Name;
        public override string FullName => _metadata.FullName;

        private IEnumerable<Class> _classes;
        public override IEnumerable<Class> Classes => _classes ??= ClassImpl.FromMetadata(_metadata.Classes, this);

        private DelegateCollection _delegates;
        public override DelegateCollection Delegates => _delegates ??= DelegateImpl.FromMetadata(_metadata.Delegates, this);

        private IEnumerable<Enum> _enums;
        public override IEnumerable<Enum> Enums => _enums ??= EnumImpl.FromMetadata(_metadata.Enums, this);

        private IEnumerable<Interface> _interfaces;
        public override IEnumerable<Interface> Interfaces => _interfaces ??= InterfaceImpl.FromMetadata(_metadata.Interfaces, this);

        public override string ToString()
        {
            return Name;
        }
    }
}
