﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using DynamicCode.SourceGenerator.Metadata.Interfaces;

namespace DynamicCode.SourceGenerator.Metadata.Roslyn
{
    public class RoslynConstantMetadata : RoslynFieldMetadata, IConstantMetadata
    {
        private readonly IFieldSymbol symbol;

        private RoslynConstantMetadata(IFieldSymbol symbol) : base(symbol)
        {
            this.symbol = symbol;
        }

        public string Value => $"{symbol.ConstantValue}";

        public new static IEnumerable<IConstantMetadata> FromFieldSymbols(IEnumerable<IFieldSymbol> symbols)
        {
            return symbols.Where(s => s.DeclaredAccessibility == Accessibility.Public && s.IsConst).Select(s => new RoslynConstantMetadata(s));
        }
    }
}