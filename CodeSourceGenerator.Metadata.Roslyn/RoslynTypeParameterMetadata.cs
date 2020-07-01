using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using CodeSourceGenerator.Metadata.Interfaces;

namespace CodeSourceGenerator.Metadata.Roslyn
{
    public class RoslynTypeParameterMetadata : ITypeParameterMetadata
    {
        private readonly ITypeParameterSymbol symbol;

        public RoslynTypeParameterMetadata(ITypeParameterSymbol symbol)
        {
            this.symbol = symbol;
        }

        public string Name => symbol.Name;

        public static IEnumerable<ITypeParameterMetadata> FromTypeParameterSymbols(IEnumerable<ITypeParameterSymbol> symbols)
        {
            return symbols.Select(s => new RoslynTypeParameterMetadata(s));
        }
    }
}