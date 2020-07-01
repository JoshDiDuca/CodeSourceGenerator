using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using CodeSourceGenerator.Metadata.Interfaces;

namespace CodeSourceGenerator.Metadata.Roslyn
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
            return symbols.Where(s => s.IsConst).Select(s => new RoslynConstantMetadata(s));
        }
    }
}