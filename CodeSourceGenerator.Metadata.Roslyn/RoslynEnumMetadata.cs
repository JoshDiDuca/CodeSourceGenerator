using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using CodeSourceGenerator.Metadata.Interfaces;

namespace CodeSourceGenerator.Metadata.Roslyn
{
    public class RoslynEnumMetadata : IEnumMetadata
    {
        private readonly INamedTypeSymbol _symbol;
        private readonly IFileMetadata _file;

        public RoslynEnumMetadata(INamedTypeSymbol symbol, IFileMetadata file = null)
        {
            _symbol = symbol;
            _file = file;
        }

        public string DocComment => _symbol.GetDocumentationCommentXml();
        public string Name => _symbol.Name;
        public string FullName => _symbol.ToDisplayString();
        public string Namespace => _symbol.GetNamespace();

        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(_symbol);

        public bool IsPublic => _symbol.DeclaredAccessibility == Accessibility.Public;
        public bool IsPrivate => _symbol.DeclaredAccessibility == Accessibility.Private;
        public bool IsProtected => _symbol.DeclaredAccessibility == Accessibility.Protected;

        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(_symbol.GetAttributes());
        public IClassMetadata ContainingClass => RoslynClassMetadata.FromNamedTypeSymbol(_symbol.ContainingType);
        public IEnumerable<IEnumValueMetadata> Values => RoslynEnumValueMetadata.FromFieldSymbols(_symbol.GetMembers().OfType<IFieldSymbol>());
        
        internal static IEnumerable<IEnumMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols, RoslynFileMetadata file = null)
        {
            return symbols.Select(s => new RoslynEnumMetadata(s, file));
        }
    }
}