﻿using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Metadata.Interfaces
{
    public interface IFileMetadata
    {
        string Name { get; }
        string FullName { get; }
        IEnumerable<IClassMetadata> Classes { get; }
        IEnumerable<IDelegateMetadata> Delegates { get; }
        IEnumerable<IEnumMetadata> Enums { get; }
        IEnumerable<IInterfaceMetadata> Interfaces { get; }
    }
}