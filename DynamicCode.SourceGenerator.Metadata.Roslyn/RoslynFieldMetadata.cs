using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using DynamicCode.SourceGenerator.Metadata.Interfaces;

namespace DynamicCode.SourceGenerator.Metadata.Roslyn
{
    public class RoslynFieldMetadata : IFieldMetadata
    {
        private readonly IFieldSymbol symbol;

        public RoslynFieldMetadata(IFieldSymbol symbol)
        {
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public bool IsPublic => symbol.DeclaredAccessibility == Accessibility.Public;
        public bool IsPrivate => symbol.DeclaredAccessibility == Accessibility.Private;
        public bool IsProtected => symbol.DeclaredAccessibility == Accessibility.Protected;
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes());
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.Type);

        public static IEnumerable<IFieldMetadata> FromFieldSymbols(IEnumerable<IFieldSymbol> symbols)
        {
            return symbols.Where(s => s.CanBeReferencedByName).Select(s => new RoslynFieldMetadata(s));
        }
    }
}