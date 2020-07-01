﻿using System.Collections.Generic;

namespace CodeSourceGenerator.Metadata.Interfaces
{
    public interface IEnumValueMetadata : INamedItem
    {
        string DocComment { get; }
        IEnumerable<IAttributeMetadata> Attributes { get; }
        long Value { get; }
    }
}