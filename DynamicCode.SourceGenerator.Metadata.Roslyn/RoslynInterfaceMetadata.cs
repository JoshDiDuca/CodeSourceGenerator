using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using DynamicCode.SourceGenerator.Metadata.Interfaces;

namespace DynamicCode.SourceGenerator.Metadata.Roslyn
{
    public class RoslynInterfaceMetadata : IInterfaceMetadata
    {
        private readonly INamedTypeSymbol symbol;
        private readonly RoslynFileMetadata _file;

        public RoslynInterfaceMetadata(INamedTypeSymbol symbol, RoslynFileMetadata file = null)
        {
            this.symbol = symbol;
            _file = file;
        }

        private IReadOnlyCollection<ISymbol> _members;
        private IReadOnlyCollection<ISymbol> Members
        {
            get
            {
                if (_members == null)
                {
                    _members = symbol.GetMembers();
                }
                return _members;
            }
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public bool IsGeneric => symbol.TypeParameters.Any();
        public string Namespace => symbol.GetNamespace();
        public bool IsPublic => symbol.DeclaredAccessibility == Accessibility.Public;
        public bool IsPrivate => symbol.DeclaredAccessibility == Accessibility.Private;
        public bool IsProtected => symbol.DeclaredAccessibility == Accessibility.Protected;

        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol);

        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes());
        public IClassMetadata ContainingClass => RoslynClassMetadata.FromNamedTypeSymbol(symbol.ContainingType);
        public IEnumerable<IEventMetadata> Events => RoslynEventMetadata.FromEventSymbols(Members.OfType<IEventSymbol>());
        public IEnumerable<IInterfaceMetadata> Interfaces => RoslynInterfaceMetadata.FromNamedTypeSymbols(symbol.Interfaces);
        public IEnumerable<IMethodMetadata> Methods => RoslynMethodMetadata.FromMethodSymbols(Members.OfType<IMethodSymbol>());
        public IEnumerable<IPropertyMetadata> Properties => RoslynPropertyMetadata.FromPropertySymbol(Members.OfType<IPropertySymbol>());
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(symbol.TypeParameters);
        public IEnumerable<ITypeMetadata> TypeArguments => RoslynTypeMetadata.FromTypeSymbols(symbol.TypeArguments);

        public static IEnumerable<IInterfaceMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols, RoslynFileMetadata file = null)
        {
            return symbols.Select(s => new RoslynInterfaceMetadata(s, file));
        }
    }
}