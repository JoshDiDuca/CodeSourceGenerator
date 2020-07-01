using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using CodeSourceGenerator.Metadata.Interfaces;

namespace CodeSourceGenerator.Metadata.Roslyn
{
    public class RoslynParameterMetadata : IParameterMetadata
    {
        private readonly IParameterSymbol symbol;

        private RoslynParameterMetadata(IParameterSymbol symbol)
        {
            this.symbol = symbol;
        }

        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public bool HasDefaultValue => symbol.HasExplicitDefaultValue;
        public string DefaultValue => GetDefaultValue();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes());
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.Type);

        private string GetDefaultValue()
        {
            if (symbol.HasExplicitDefaultValue == false)
                return null;

            if (symbol.ExplicitDefaultValue == null)
                return "null";

            if (symbol.ExplicitDefaultValue is string stringValue)
                return $"\"{stringValue.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";

            if (symbol.ExplicitDefaultValue is bool boolean)
                return boolean ? "true" : "false";

            return symbol.ExplicitDefaultValue.ToString();
        }

        public static IEnumerable<IParameterMetadata> FromParameterSymbols(IEnumerable<IParameterSymbol> symbols)
        {
            return symbols.Select(s => new RoslynParameterMetadata(s));
        }
    }
}