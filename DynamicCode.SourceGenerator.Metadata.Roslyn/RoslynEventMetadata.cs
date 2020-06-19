using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using DynamicCode.SourceGenerator.Metadata.Interfaces;

namespace DynamicCode.SourceGenerator.Metadata.Roslyn
{
    public class RoslynEventMetadata : IEventMetadata
    {
        private readonly IEventSymbol symbol;

        public RoslynEventMetadata(IEventSymbol symbol)
        {
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes());
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.Type);
        public bool IsPublic => symbol.DeclaredAccessibility == Accessibility.Public;
        public bool IsPrivate => symbol.DeclaredAccessibility == Accessibility.Private;
        public bool IsProtected => symbol.DeclaredAccessibility == Accessibility.Protected;

        public static IEnumerable<IEventMetadata> FromEventSymbols(IEnumerable<IEventSymbol> symbols)
        {
            return symbols.Where(s => s.IsStatic == false).Select(s => new RoslynEventMetadata(s));
        }
    }
}
