using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using DynamicCode.SourceGenerator.Metadata.Interfaces;

namespace DynamicCode.SourceGenerator.Metadata.Roslyn
{
    public class RoslynInterfaceMetadata : IInterfaceMetadata
    {
        private readonly INamedTypeSymbol _symbol;
        private readonly RoslynFileMetadata _file;

        public RoslynInterfaceMetadata(INamedTypeSymbol symbol, RoslynFileMetadata file = null)
        {
            _symbol = symbol;
            _file = file;
        }

        private IReadOnlyCollection<ISymbol> _members;
        private IReadOnlyCollection<ISymbol> Members
        {
            get
            {
                if (_members == null)
                {
                    _members = _symbol.GetMembers();
                }
                return _members;
            }
        }

        public string DocComment => _symbol.GetDocumentationCommentXml();
        public string Name => _symbol.Name;
        public string FullName => _symbol.ToDisplayString();
        public bool IsGeneric => _symbol.TypeParameters.Any();
        public string Namespace => _symbol.GetNamespace();

        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(_symbol);

        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(_symbol.GetAttributes());
        public IClassMetadata ContainingClass => RoslynClassMetadata.FromNamedTypeSymbol(_symbol.ContainingType);
        public IEnumerable<IEventMetadata> Events => RoslynEventMetadata.FromEventSymbols(Members.OfType<IEventSymbol>());
        public IEnumerable<IInterfaceMetadata> Interfaces => RoslynInterfaceMetadata.FromNamedTypeSymbols(_symbol.Interfaces);
        public IEnumerable<IMethodMetadata> Methods => RoslynMethodMetadata.FromMethodSymbols(Members.OfType<IMethodSymbol>());
        public IEnumerable<IPropertyMetadata> Properties => RoslynPropertyMetadata.FromPropertySymbol(Members.OfType<IPropertySymbol>());
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(_symbol.TypeParameters);
        public IEnumerable<ITypeMetadata> TypeArguments => RoslynTypeMetadata.FromTypeSymbols(_symbol.TypeArguments);

        public static IEnumerable<IInterfaceMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols, RoslynFileMetadata file = null)
        {
            return symbols.Where(s => s.DeclaredAccessibility == Accessibility.Public).Select(s => new RoslynInterfaceMetadata(s, file));
        }
    }
}